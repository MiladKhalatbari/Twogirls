using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Context;
using TwoGirls.DataLayer.Entities;
using Address = TwoGirls.DataLayer.Entities.Address;
using Product = TwoGirls.DataLayer.Entities.Product;

namespace TwoGirls.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly TwogirsContext _context;

        public OrderService(TwogirsContext context)
        {
            _context = context;
        }

        #region Cart
        public Cart GetOpenCartIncludeProductsOrCreateNew(int userId)
        {
            var cart = _context.Carts
                  .Include(c => c.CartItems)
                      .ThenInclude(ci => ci.Product)
                          .ThenInclude(p => p.ImagePaths)
                  .FirstOrDefault(c => c.UserId == userId && !c.IsClose);

            if (cart == null)
            {
                cart = new Cart
                {
                    IsClose = false,
                    UserId = userId,
                    CreateDate = DateTime.Now
                };
                AddCartAndOrder(cart);

                cart = _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                            .ThenInclude(p => p.ImagePaths)
                    .FirstOrDefault(c => c.UserId == userId && !c.IsClose);
            }
            else
            {
                if (!CheckOrderExistAlreadyByCartId(cart.Id))
                {
                    var order = new Order()
                    {
                        IsPosted = false,
                        UserId = cart.UserId,
                        OrderDate = DateTime.Now,
                        CartId = cart.Id,
                        OrderPrice = cart.TotalOrderPrice(),
                    };
                    AddOrder(order);
                }
            }
            return cart;
        }
        public Cart GetCartByIdIncludeProducts(int userId, int cardId)
        {
            return _context.Carts
                  .Include(c => c.CartItems)
                      .ThenInclude(ci => ci.Product)
                          .ThenInclude(p => p.ImagePaths)
                  .FirstOrDefault(c => c.UserId == userId && c.Id == cardId);

        }
        public Cart GetExistingOpenCartIncludeProducts(int userId)
        {
            return _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId && x.IsClose == false);
        }
        public Cart GetExistingOpenCartIncludeProducts(int userId, int cartId)
        {
            return _context.Carts.
                Include(x => x.CartItems).
                ThenInclude(x => x.Product).
                ThenInclude(p => p.ImagePaths).
                FirstOrDefault(x => x.UserId == userId && x.IsClose == false && x.Id == cartId);
        }
        public Cart GetCartByIdForAdminIncludeProducts(int cardId)
        {
            return _context.Carts
                   .Include(c => c.CartItems)
                       .ThenInclude(ci => ci.Product)
                           .ThenInclude(p => p.ImagePaths)
                   .FirstOrDefault(c => c.Id == cardId);
        }

        public void UpdateCart(Cart cart)
        {
            _context.Update(cart);
            _context.SaveChanges();
        }
        public int AddCartAndOrder(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
            if (!CheckOrderExistAlreadyByCartId(cart.Id))
            {
                var order = new Order()
                {
                    IsPosted = false,
                    UserId = cart.UserId,
                    OrderDate = DateTime.Now,
                    CartId = cart.Id,
                    OrderPrice = cart.TotalOrderPrice(),
                };
                AddOrder(order);
            }
            return cart.Id;
        }
        #endregion

        #region CartItem
        public void AddProductToCartItem(Product product, int userId)
        {

            var cart = GetOpenCartIncludeProductsOrCreateNew(userId);
            cart.AddCardItem(product);
            _context.SaveChanges();
            UpdateOrderPrice(cart.Id, userId);
            _context.SaveChanges();
        }
        public void DeleteProductFromCartItem(Product product, int userId)
        {
            var cart = _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId && x.IsClose == false);
            if (product != null && cart != null)
            {
                cart.RemoveCardItem(product);
                _context.Update(cart);
                _context.SaveChanges();
                UpdateOrderPrice(cart.Id, userId);
                _context.SaveChanges();
            }
        }
        #endregion

        #region Order
        public List<Order> GetAllOrdersByUserId(int UserId)
        {
            return _context.Orders.Where(o => o.UserId == UserId && o.IsFinally).ToList();
        }
        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
        public int AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.OrderId;
        }
        public bool CheckOrderExistAlreadyByCartId(int cartId)
        {
            return _context.Orders.Any(x => x.CartId == cartId);
        }
        public Order GetOrderByCartId(int cartId)
        {
            return _context.Orders.First(x => x.CartId == cartId);
        }
        public Address GetOrderAddressByCartId(int cartId)
        {
            return _context.Orders
                 .Where(o => o.CartId == cartId)
                 .Include(o => o.Address)
                 .FirstOrDefault()?.Address;
        }
        public void UpdateOrderPrice(int cartId, int userId)
        {
            var order = GetOrderByCartId(cartId);
            var cart = GetCartByIdIncludeProducts(userId, cartId);
            var valuediscounted = (int)cart.TotalOrderPrice() * order.DiscountPercent / 100;
            order.OrderPrice = (int)cart.TotalOrderPrice() - valuediscounted;
            _context.SaveChanges();
        }
        public OrdersForAdminViewModel GetAllOrdersForAdmin(int pageId, string filter)
        {
            var take = 1;
            var skip = (pageId - 1) * take;
            IQueryable<Order> orders = _context.Orders.Where(o => !o.IsPosted && o.IsFinally).Include(o => o.User);
            if (!filter.IsNullOrEmpty())
            {
                orders = orders.Where(o => o.User.FirstName.ToLower().Contains(filter.ToLower()) || o.User.LastName.ToLower().Contains(filter.ToLower()));
            }
            var ordersForAdmin = new OrdersForAdminViewModel()
            {
                Orders = orders.OrderBy(x => x.OrderDate).Skip(skip).Take(take).ToList(),
                PageCount = (int)Math.Ceiling(orders.AsEnumerable().Count() / (double)take),
                CurrentPage = pageId,
                Filter = filter
            };
            return ordersForAdmin;
        }
        public OrdersForAdminViewModel GetAllSheppedOrdersForAdmin(int pageId, string filter)
        {
            var take = 1;
            var skip = (pageId - 1) * take;
            IQueryable<Order> orders = _context.Orders.Where(o => o.IsPosted && o.IsFinally).Include(o => o.User);
            if (!filter.IsNullOrEmpty())
            {
                orders = orders.Where(o => o.User.FirstName.ToLower().Contains(filter.ToLower()) || o.User.LastName.ToLower().Contains(filter.ToLower()));
            }
            var ordersForAdmin = new OrdersForAdminViewModel()
            {
                Orders = orders.OrderBy(x => x.OrderDate).Skip(skip).Take(take).ToList(),
                PageCount = (int)Math.Ceiling(orders.AsEnumerable().Count() / (double)take),
                CurrentPage = pageId,
                Filter = filter
            };
            return ordersForAdmin;
        }

        #endregion

        #region Discount
        public DiscountResultType UseDiscountCode(int userId, string discountCode, int cartId)
        {
            var discount = _context.DiscountCodes.SingleOrDefault(x => x.Code == discountCode);
            if (discount == null)
            {
                return DiscountResultType.NotFound;
            }
            else if (discount.StartDate > DateTime.Now)
            {
                return DiscountResultType.NotStarted;
            }
            else if (discount.EendDate < DateTime.Now)
            {
                return DiscountResultType.Expired;
            }
            else if (discount.UseableCount < 1)
            {
                return DiscountResultType.Finished;
            }
            else if (_context.UserDiscountCodes.Any(dc => dc.UserId == userId && dc.DiscountId == discount.DiscountId))
            {
                return DiscountResultType.UseTwice;
            }
            else
            {
                var order = GetOrderByCartId(cartId);
                order.DiscountPercent = discount.DiscountPercent;
                UpdateOrderPrice(cartId, userId);
                var userDiscountCodes = new UserDiscountCodes()
                {
                    DiscountId = discount.DiscountId,
                    UserId = userId,
                };
                _context.UserDiscountCodes.Add(userDiscountCodes);
                discount.UseableCount = discount.UseableCount - 1;
                _context.SaveChanges();
                return DiscountResultType.Success;
            }
        }
        public DiscountCode GetDiscountCodeById(int discountId)
        {
            return _context.DiscountCodes.FirstOrDefault(d => d.DiscountId == discountId);
        }
        public DiscountCode GetDiscountCodeByIdIgnoreQueryFilters(int discountId)
        {
            return _context.DiscountCodes.IgnoreQueryFilters().FirstOrDefault(dc => dc.DiscountId == discountId);
        }
        public void AddDiscountCode(DiscountCode discountCode)
        {
            _context.DiscountCodes.Add(discountCode);
            _context.SaveChanges();
        }
        public void UpdateDiscountCode(DiscountCode discountCode)
        {
            _context.DiscountCodes.Update(discountCode);
            _context.SaveChanges();
        }
        public bool DeleteDiscountForAdmin(int discountId)
        {
            try
            {
                var dc = GetDiscountCodeById(discountId);
                dc.IsDelete = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool RecoverDiscountForAdmin(int discountId)
        {
            try
            {
                var dc = GetDiscountCodeByIdIgnoreQueryFilters(discountId);
                dc.IsDelete = false;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public DiscountsForAdminViewModel GetAllDiscountCodes(string filter, int pageId)
        {
            IQueryable<DiscountCode> discounts = _context.DiscountCodes;
            var take = 1;
            var skip = (pageId - 1) * take;
            if (filter.IsNullOrEmpty())
            {
                discounts.Where(o => o.Code.ToLower().Contains(filter.ToLower()) || o.DiscountPercent == int.Parse(filter));
            }
            var discountsForAdmin = new DiscountsForAdminViewModel()
            {
                CurrentPage = pageId,
                Filter = filter,
                PageCount = (int)Math.Ceiling(discounts.AsEnumerable().Count() / (double)take),
                Discounts = discounts.OrderByDescending(dc => dc.StartDate).Skip(skip).Take(take).ToList()
            };

            return discountsForAdmin;
        }
        public DiscountsForAdminViewModel GetAllRemovedDiscountCodesIgnoreQueryFilters(string filter, int pageId)
        {
            var discounts = _context.DiscountCodes.IgnoreQueryFilters().Where(dc => dc.IsDelete);
            var take = 1;
            var skip = (pageId - 1) * take;
            if (filter.IsNullOrEmpty())
            {
                discounts.Where(o => o.Code.ToLower().Contains(filter.ToLower()) || o.DiscountPercent == int.Parse(filter));
            }
            var discountsForAdmin = new DiscountsForAdminViewModel()
            {
                CurrentPage = pageId,
                Filter = filter,
                PageCount = (int)Math.Ceiling(discounts.AsEnumerable().Count() / (double)take),
                Discounts = discounts.OrderByDescending(dc => dc.StartDate).Skip(skip).Take(take).ToList()
            };

            return discountsForAdmin;
        }
        #endregion

        #region Transaction
        public TransactionsForAdminViewModel GetAllTransactionsForAdmin(string filter, int pageId)
        {
            IQueryable<Transaction> transactions = _context.Transactions.Include(t => t.User).Include(t => t.TransactionType);
            var take = 1;
            var skip = (pageId - 1) * take;
            if (filter.IsNullOrEmpty())
            {
                transactions.Where(t => t.User.FirstName.ToLower().Contains(filter.ToLower()) || t.User.LastName.ToLower().Contains(filter.ToLower()) || (t.Description != null && t.Description.ToLower().Contains(filter.ToLower())));
            }
            var TransactionsForAdminViewModel = new TransactionsForAdminViewModel()
            {
                CurrentPage = pageId,
                Filter = filter,
                PageCount = (int)Math.Ceiling(transactions.AsEnumerable().Count() / (double)take),
                Transactions = transactions.OrderByDescending(t => t.Date).Skip(skip).Take(take).ToList()
            };

            return TransactionsForAdminViewModel;
        }
        public TransactionsForAdminViewModel GetDeletedTransactionsForAdminIgnoreQueryFilters(string filter, int pageId)
        {
            IQueryable<Transaction> transactions = _context.Transactions.IgnoreQueryFilters().Where(dc => dc.IsDelete).Include(t => t.User).Include(t => t.TransactionType);
            var take = 1;
            var skip = (pageId - 1) * take;
            if (filter.IsNullOrEmpty())
            {
                transactions.Where(t => t.User.FirstName.ToLower().Contains(filter.ToLower()) || t.User.LastName.ToLower().Contains(filter.ToLower()) || (t.Description != null && t.Description.ToLower().Contains(filter.ToLower())));
            }
            var TransactionsForAdminViewModel = new TransactionsForAdminViewModel()
            {
                CurrentPage = pageId,
                Filter = filter,
                PageCount = (int)Math.Ceiling(transactions.AsEnumerable().Count() / (double)take),
                Transactions = transactions.OrderByDescending(t => t.Date).Skip(skip).Take(take).ToList()
            };

            return TransactionsForAdminViewModel;
        }
        public Transaction GetTransactionByIdIgnoreQueryFilters(int transactionId)
        {
            return _context.Transactions.IgnoreQueryFilters().Include(t => t.User).Include(t => t.TransactionType).FirstOrDefault(t => t.Id == transactionId);
        }
        public Transaction GetTransactionById(int transactionId)
        {
            return _context.Transactions.Include(t => t.User).Include(t => t.TransactionType).FirstOrDefault(t => t.Id == transactionId);

        }
        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
        public void UpdateTransaction(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }
        public bool DeleteTransactionForAdmin(int transactionId)
        {
            try
            {
                var tr = GetTransactionById(transactionId);
                tr.IsDelete = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool RecoverTransactionForAdmin(int transactionId)
        {

            try
            {
                var tr = GetTransactionByIdIgnoreQueryFilters(transactionId);
                tr.IsDelete = false;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        #endregion

        #region Transaction Type
        public List<TransactionType> GetAllTransactionsType()
        {
            return _context.TransactionTypes.ToList();
        }
        #endregion

    }
}





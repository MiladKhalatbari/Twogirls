using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Context;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.Services
{
    public class OrderService : IOrderService
    {
        private TwogirsContext _context;

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
                    Order order = new Order()
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
                ThenInclude(p=> p.ImagePaths).
                FirstOrDefault(x => x.UserId == userId && x.IsClose == false && x.Id == cartId);
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
                Order order = new Order()
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
            var cart = GetCartByIdIncludeProducts(userId,cartId);
            int valuediscounted = (int)cart.TotalOrderPrice() * order.DiscountPercent / 100;
            order.OrderPrice = (int)cart.TotalOrderPrice() - valuediscounted;
            _context.SaveChanges();
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
        public bool CheckOrderExistAlreadyByCartId(int cartId)
        {
            return _context.Orders.Any(x => x.CartId == cartId);
        }
        #endregion

    }
}


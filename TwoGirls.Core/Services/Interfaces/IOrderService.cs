using TwoGirls.Core.DTOs;
using TwoGirls.DataLayer.Entities;
using Address = TwoGirls.DataLayer.Entities.Address;
using Product = TwoGirls.DataLayer.Entities.Product;

namespace TwoGirls.Core.Services.Interfaces
{
    public interface IOrderService
    {
        #region Cart
        public Cart GetCartByIdIncludeProducts(int userId, int cardId);
        public Cart GetOpenCartIncludeProductsOrCreateNew(int userId);
        public Cart GetExistingOpenCartIncludeProducts(int userId);
        public Cart GetExistingOpenCartIncludeProducts(int userId, int cartId);
        public Cart GetCartByIdForAdminIncludeProducts(int cardId);
        public void UpdateCart(Cart cart);
        public int AddCartAndOrder(Cart cart);
        #endregion

        #region CartItem
        public void AddProductToCartItem(Product product, int userId);
        public void DeleteProductFromCartItem(Product product, int userId);
        #endregion

        #region Order
        public int AddOrder(Order order);
        public void UpdateOrder(Order order);
        public void UpdateOrderPrice(int cartId, int userId);
        public Order GetOrderByCartId(int cartId);
        public bool CheckOrderExistAlreadyByCartId(int cartId);
        public List<Order> GetAllOrdersByUserId(int UserId)
; public Address GetOrderAddressByCartId(int cartId);
        public OrdersForAdminViewModel GetAllOrdersForAdmin(int pageId, string filter);
        public OrdersForAdminViewModel GetAllSheppedOrdersForAdmin(int pageId, string filter);
        #endregion

        #region Discount
        public DiscountResultType UseDiscountCode(int userId, string discountCode, int cartId);
        public DiscountCode GetDiscountCodeByIdIgnoreQueryFilters(int discountId);
        public DiscountCode GetDiscountCodeById(int iddiscountId);
        public void AddDiscountCode(DiscountCode discountCode);
        public void UpdateDiscountCode(DiscountCode discountCode);
        public bool DeleteDiscountForAdmin(int discountId);
        public bool RecoverDiscountForAdmin(int discountId);
        public DiscountsForAdminViewModel GetAllDiscountCodes(string filter, int pageId);
        public DiscountsForAdminViewModel GetAllRemovedDiscountCodesIgnoreQueryFilters(string filter, int pageId);
        #endregion

        #region Transaction
        public TransactionsForAdminViewModel GetAllTransactionsForAdmin(string filter, int pageId);
        public TransactionsForAdminViewModel GetDeletedTransactionsForAdminIgnoreQueryFilters(string filter, int pageId);
        public Transaction GetTransactionByIdIgnoreQueryFilters(int transactionId);
        public Transaction GetTransactionById(int transactionId);
        public void AddTransaction(Transaction transaction);
        public void UpdateTransaction(Transaction transaction);
        public bool DeleteTransactionForAdmin(int transactionId);
        public bool RecoverTransactionForAdmin(int transactionId);
        #endregion

        #region Transaction Type
        public List<TransactionType> GetAllTransactionsType();
        #endregion
    }
}

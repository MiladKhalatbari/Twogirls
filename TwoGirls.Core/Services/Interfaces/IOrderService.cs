using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoGirls.Core.DTOs;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.Services.Interfaces
{
    public interface IOrderService
    {
        #region Cart
        public Cart GetCartByIdIncludeProducts(int userId, int cardId);
        public Cart GetOpenCartIncludeProductsOrCreateNew(int userId);
        public Cart GetExistingOpenCartIncludeProducts(int userId);
        public Cart GetExistingOpenCartIncludeProducts(int userId,int cartId);
        public void UpdateCart(Cart cart);
        public int AddCartAndOrder(Cart cart);
        #endregion

        #region CartItem
        public void AddProductToCartItem(DataLayer.Entities.Product product, int userId);
        public void DeleteProductFromCartItem(DataLayer.Entities.Product product, int userId);
        #endregion

        #region Order
        public int AddOrder(Order order);
        public void UpdateOrder(Order order);
        public void UpdateOrderPrice(int cartId, int userId);
        public Order GetOrderByCartId(int cartId);
        public bool CheckOrderExistAlreadyByCartId(int cartId);
        public List<Order> GetAllOrdersByUserId(int UserId)
;       public DataLayer.Entities.Address GetOrderAddressByCartId(int cartId);
        #endregion

        #region Discount
        public DiscountResultType UseDiscountCode(int userId, string discountCode, int cartId);
        #endregion

    }
}

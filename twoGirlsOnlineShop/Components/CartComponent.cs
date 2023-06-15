
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TwoGirls.DataLayer.Entities;
using TwoGirls.DataLayer.Context;
using twoGirlsOnlineShop.Models;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Components
{
    public class CartComponent : ViewComponent
    {
        private IOrderService _orderService;

        public CartComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IViewComponentResult> InvokeAsync(Cart cart)
        {
            if (cart == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    cart = _orderService.GetOpenCartIncludeProductsOrCreateNew(userId);
                }
                else
                {
                    return View("CartComponent", new Cart());
                }
            }
            cart.Order = _orderService.GetOrderByCartId(cart.Id);
            return View("CartComponent", cart);
        }
    }
}

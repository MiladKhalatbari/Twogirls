using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Components
{
    public class CartComponent : ViewComponent
    {
        private readonly IOrderService _orderService;

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
                    if (int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
                    {
                        cart = _orderService.GetOpenCartIncludeProductsOrCreateNew(userId);
                    }
                    else
                    {
                        return Content("Unable to retrieve user ID.");
                    }
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

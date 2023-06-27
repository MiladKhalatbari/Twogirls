using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Orders
{
    [PermissionChecker(22)]
    public class CancelShippingModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public CancelShippingModel(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        public OrderViewModel OrderView { get; set; }
        public IActionResult OnGet(int id)
        {
            OrderView = new OrderViewModel();
            var cart = _orderService.GetCartByIdForAdminIncludeProducts(id);
            if (cart != null)
            {
                cart.Order = _orderService.GetOrderByCartId(cart.Id);
                OrderView.Cart = cart;
                OrderView.Addresses = _userService.GetUserAddresses(cart.UserId);
            }
            return Page();
        }
        public IActionResult OnPost(int id)
        {

            var order = _orderService.GetOrderByCartId(id);
            order.IsPosted = false;
            _orderService.UpdateOrder(order);
            return RedirectToPage("index");
        }
    }
}

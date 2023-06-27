using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Orders
{
    [PermissionChecker(23)]
    public class ShippedOrdersModel : PageModel
    {
        private readonly IOrderService _orderService;
        public ShippedOrdersModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public OrdersForAdminViewModel ordersForAdmin { get; set; }

        public void OnGet(string filter = "", int pageId = 1)
        {
            ordersForAdmin = _orderService.GetAllSheppedOrdersForAdmin(pageId, filter);
        }
    }
}
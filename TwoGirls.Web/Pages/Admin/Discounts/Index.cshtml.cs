using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Discounts
{
    [PermissionChecker(6)]
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public DiscountsForAdminViewModel discounts { get; set; }
        public void OnGet(string filter = "", int pageId = 1)
        {
            discounts = _orderService.GetAllDiscountCodes(filter, pageId);
        }
    }
}

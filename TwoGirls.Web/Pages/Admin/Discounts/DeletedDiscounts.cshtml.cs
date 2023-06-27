using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Discounts
{
    [PermissionChecker(27)]
    public class DeletedDiscountsModel : PageModel
    {
        private readonly IOrderService _orderService;
        public DeletedDiscountsModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public DiscountsForAdminViewModel discounts { get; set; }
        public void OnGet(string filter = "", int pageId = 1)
        {
            discounts = _orderService.GetAllRemovedDiscountCodesIgnoreQueryFilters(filter, pageId);
        }
    }
}

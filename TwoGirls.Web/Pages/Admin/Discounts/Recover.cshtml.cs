using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Discounts
{
    [PermissionChecker(26)]
    public class RecoverModel : PageModel
    {
        private readonly IOrderService _orderService;
        public RecoverModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public DiscountCode Discount { get; set; }
        public void OnGet(int id)
        {
            Discount = _orderService.GetDiscountCodeByIdIgnoreQueryFilters(id);
        }
        public IActionResult OnPost(int discountId)
        {
            _orderService.RecoverDiscountForAdmin(discountId);
            return RedirectToPage("index");
        }
    }
}

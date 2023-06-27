using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Discounts
{
    [PermissionChecker(25)]
    public class DeleteModel : PageModel
    {
        private readonly IOrderService _orderService;
        public DeleteModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public DiscountCode Discount { get; set; }
        public void OnGet(int id)
        {
            Discount = _orderService.GetDiscountCodeById(id);
        }
        public IActionResult OnPost(int discountId)
        {
            if (_orderService.DeleteDiscountForAdmin(discountId))
            {
                return RedirectToPage("index");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Discounts
{
    [PermissionChecker(24)]
    public class CreateOrEditModel : PageModel
    {
        private readonly IOrderService _orderService;
        public CreateOrEditModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public DiscountCode Discount { get; set; }
        public IActionResult OnGet(int id = 0)
        {
            Discount = new DiscountCode();
            if (id != 0)
            {
                Discount = _orderService.GetDiscountCodeById(id);
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var dc = new DiscountCode()
            {
                Code = Discount.Code,
                StartDate = Discount.StartDate,
                EendDate = Discount.EendDate,
                DiscountPercent = Discount.DiscountPercent,
                UseableCount = Discount.UseableCount
            };
            if (Discount.DiscountId == 0)
            {
                _orderService.AddDiscountCode(dc);
            }
            else
            {
                dc.DiscountId = Discount.DiscountId;
                _orderService.UpdateDiscountCode(dc);

            }
            return RedirectToPage("index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Transactions
{
    [PermissionChecker(33)]
    public class DeleteModel : PageModel
    {
        private readonly IOrderService _orderService;
        public DeleteModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public Transaction? Transaction { get; set; }
        public void OnGet(int id)
        {
            Transaction = _orderService.GetTransactionById(id);
        }
        public IActionResult OnPost(int id)
        {
            if (_orderService.DeleteTransactionForAdmin(id))
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

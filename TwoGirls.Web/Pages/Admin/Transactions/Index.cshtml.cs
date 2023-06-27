using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Transactions
{
    [PermissionChecker(8)]
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public TransactionsForAdminViewModel transactionsForAdmin { get; set; }
        public void OnGet(string filter = "", int pageId = 1)
        {
            transactionsForAdmin = _orderService.GetAllTransactionsForAdmin(filter, pageId);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Transactions
{
    [PermissionChecker(35)]
    public class DeletedTransactionsModel : PageModel
    {
        private readonly IOrderService _orderService;
        public DeletedTransactionsModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public TransactionsForAdminViewModel transactionsForAdmin { get; set; }
        public void OnGet(string filter = "", int pageId = 1)
        {
            transactionsForAdmin = _orderService.GetDeletedTransactionsForAdminIgnoreQueryFilters(filter, pageId);
        }
    }
}

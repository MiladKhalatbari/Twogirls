using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Transactions
{
    [PermissionChecker(32)]
    public class CreateOrEditModel : PageModel
    {
        private readonly IOrderService _orderService;
        public CreateOrEditModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public Transaction Transaction { get; set; }
        public IActionResult OnGet(int userid = 0, int id = 0)
        {
            Transaction = new Transaction();
            if (id != 0)
            {
                Transaction = _orderService.GetTransactionById(id);
            }
            else if (userid != 0)
            {
                Transaction = new Transaction()
                {
                    UserId = userid
                };
            }
            else
            {
                return NotFound();
            }
            ViewData["TransactionTypes"] = _orderService.GetAllTransactionsType();
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var transaction = new Transaction()
            {
                TypeId = Transaction.TypeId,
                Amount = Transaction.Amount,
                Date = Transaction.Date,
                Finaly = Transaction.Finaly,
                UserId = Transaction.UserId,
                Description = Transaction.Description,

            };
            if (Transaction.Id == 0)
            {
                _orderService.AddTransaction(transaction);
            }
            else
            {
                transaction.Id = Transaction.Id;
                _orderService.UpdateTransaction(transaction);

            }
            return RedirectToPage("index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Users
{
    public class RecoverModel : PageModel
    {
        IUserService _userService;
        public RecoverModel(IUserService userService)
        {
            _userService = userService;
        }
        public User User { get; set; }
        public void OnGet(int id)
        {
            User = _userService.GetUserByIdIgnoreQueryFilters(id);
        }
        public IActionResult OnPost(int id)
        {
            _userService.RecoverUserForAdmin(id);
            return RedirectToPage("index");
        }
    }
}

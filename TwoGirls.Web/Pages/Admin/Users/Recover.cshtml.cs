using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Users
{
    [PermissionChecker(11)]
    public class RecoverModel : PageModel
    {
        private readonly IUserService _userService;
        public RecoverModel(IUserService userService)
        {
            _userService = userService;
        }
        public User? UserModel { get; set; }
        public void OnGet(int id)
        {
            UserModel = _userService.GetUserByIdIgnoreQueryFilters(id);
        }
        public IActionResult OnPost(int id)
        {
            _userService.RecoverUserForAdmin(id);
            return RedirectToPage("index");
        }
    }
}

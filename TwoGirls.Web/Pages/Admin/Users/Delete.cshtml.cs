using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Users
{
    [PermissionChecker(10)]
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;
        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }
        public User? UserModel { get; set; }
        public void OnGet(int id)
        {
            UserModel = _userService.GetUserByIdIncludeRole(id);
        }
        public IActionResult OnPost(int id)
        {
            _userService.DeleteUserForAdmin(id);
            return RedirectToPage("index");
        }
    }
}

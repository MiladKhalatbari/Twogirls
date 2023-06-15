using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Users
{
    public class DeleteModel : PageModel
    {
        IUserService _userService;
        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }
       public User User { get; set; }
        public void OnGet(int id)
        {
            User = _userService.GetUserByIdIncludeRole(id);
        }
        public IActionResult OnPost(int id)
        {
            _userService.DeleteUserForAdmin(id);
            return RedirectToPage("index");
        }
    }
}

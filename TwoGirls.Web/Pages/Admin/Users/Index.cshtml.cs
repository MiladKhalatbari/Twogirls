using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Users
{
    [PermissionChecker(2)]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        public UsersForAdminViewModel UsersForAdminViewModel { get; set; }
        public void OnGet(string filter = "", int pageId = 1)
        {
            UsersForAdminViewModel = _userService.GetUsersByFilterForAdminViewModel(pageId, filter);
            UsersForAdminViewModel.Filter = filter;
        }
    }
}

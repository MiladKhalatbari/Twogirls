using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Users
{
    [PermissionChecker(12)]
    public class DeletedUsersModel : PageModel
    {
        private readonly IUserService _userService;

        public DeletedUsersModel(IUserService userService)
        {
            _userService = userService;
        }
        public UsersForAdminViewModel UsersForAdminViewModel { get; set; }
        public void OnGet(string filter = "", int pageId = 1)
        {
            UsersForAdminViewModel = _userService.GetDeletedUsersByFilterForAdminViewModel(pageId, filter);
            UsersForAdminViewModel.Filter = filter;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Services.Interfaces;

namespace twoGirlsOnlineShop.Pages.Admin.Users
{
    public class DeletedUsersModel : PageModel
    {
        private readonly IUserService _userService;

        public DeletedUsersModel(IUserService userService)
        {
            _userService = userService;
        }
       public  UsersForAdminViewModel UsersForAdminViewModel { get; set; }
        public void OnGet( string filter = "",int pageId = 1 )
        {
            UsersForAdminViewModel = _userService.GetDeletedUsersByFilterForAdminViewModel(pageId,filter);
            UsersForAdminViewModel.Filter = filter;
        }
    }
}

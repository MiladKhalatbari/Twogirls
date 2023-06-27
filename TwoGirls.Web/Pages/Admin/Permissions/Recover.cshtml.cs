using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Permissions
{
    [PermissionChecker(15)]
    public class RecoverModel : PageModel
    {
        private readonly IPermissionService _permissionService;
        public RecoverModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public Role Role { get; set; }
        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleByIdIgnorequeryFilter(id);
        }
        public IActionResult OnPost(int id)
        {
            _permissionService.RecoverRole(id);
            return RedirectToPage("index");
        }
    }
}

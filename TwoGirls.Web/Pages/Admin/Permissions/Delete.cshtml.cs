using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Permissions
{
    [PermissionChecker(14)]
    public class DeleteModel : PageModel
    {
        private readonly IPermissionService _permissionService;
        public DeleteModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public Role Role { get; set; }
        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleById(id);
        }
        public IActionResult OnPost(int id)
        {
            _permissionService.DeleteRole(id);
            return RedirectToPage("index");
        }
    }
}

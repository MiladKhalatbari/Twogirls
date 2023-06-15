using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Permissions
{
    public class DeleteModel : PageModel
    {
        IPermissionService _permissionService;
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

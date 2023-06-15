using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Permissions
{
    public class IndexModel : PageModel
    {
        IPermissionService _permissionService;

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public ICollection<Role> Roles { get; set; }
        public void OnGet()
        {
            Roles = _permissionService.GetAllRoles();
        }
    }
}

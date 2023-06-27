using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Permissions
{
    [PermissionChecker(3)]
    public class IndexModel : PageModel
    {
        private readonly IPermissionService _permissionService;

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

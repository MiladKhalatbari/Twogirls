using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Permissions
{
    [PermissionChecker(16)]
    public class DeletedRolesModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public DeletedRolesModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public ICollection<Role> Roles { get; set; }
        public void OnGet()
        {
            Roles = _permissionService.GetAllDeletedRoles();
        }
    }
}

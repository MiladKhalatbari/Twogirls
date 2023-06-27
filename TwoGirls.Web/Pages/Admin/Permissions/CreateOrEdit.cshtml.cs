using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Permissions
{
    [PermissionChecker(13)]
    public class CreateOrEditModel : PageModel
    {
        private readonly IPermissionService _permissionService;
        public CreateOrEditModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
            CreateOrEditRoleForAdmin = new CreateOrEditRoleForAdmin();
        }
        [BindProperty]
        public CreateOrEditRoleForAdmin CreateOrEditRoleForAdmin { get; set; }
        public void OnGet(int id = 0)
        {
            CreateOrEditRoleForAdmin.Permissions = _permissionService.GetAllPermissions();
            if (id == 0)
            {
                CreateOrEditRoleForAdmin.Role = new Role();
                CreateOrEditRoleForAdmin.SelectedRoles = new List<int>();
            }
            else
            {
                CreateOrEditRoleForAdmin.SelectedRoles = _permissionService.GetRoleById(id).RolePermissions.Select(x => x.PermissionId).ToList();
                CreateOrEditRoleForAdmin.Role = _permissionService.GetRoleById(id);

            }
        }
        public IActionResult OnPost()
        {
            int roleId;
            if (string.IsNullOrEmpty(CreateOrEditRoleForAdmin.Role.RoleTitle))
            {
                CreateOrEditRoleForAdmin.Permissions = _permissionService.GetAllPermissions();
                if (CreateOrEditRoleForAdmin.Role.RoleId == 0)
                {
                    CreateOrEditRoleForAdmin.Role = new Role();
                    CreateOrEditRoleForAdmin.SelectedRoles = new List<int>();
                }
                else
                {
                    CreateOrEditRoleForAdmin.SelectedRoles = _permissionService.GetRoleById(CreateOrEditRoleForAdmin.Role.RoleId).RolePermissions.Select(x => x.PermissionId).ToList();
                    CreateOrEditRoleForAdmin.Role = _permissionService.GetRoleById(CreateOrEditRoleForAdmin.Role.RoleId);

                }
                ModelState.AddModelError("CreateOrEditRoleForAdmin.Role.RoleTitl", "Title is Required");
                return Page();
            }
            if (CreateOrEditRoleForAdmin.Role.RoleId == 0)
            {
                var role = new Role()
                {
                    RoleTitle = CreateOrEditRoleForAdmin.Role.RoleTitle,
                };
                roleId = _permissionService.AddRole(role);
                foreach (var permission in CreateOrEditRoleForAdmin.SelectedRoles)
                {
                    _permissionService.AddRolePermission(roleId, permission);
                }
            }
            else
            {
                roleId = CreateOrEditRoleForAdmin.Role.RoleId;
                _permissionService.EditRole(CreateOrEditRoleForAdmin.Role);
                _permissionService.RemoveExistingPermission(roleId);
                if (CreateOrEditRoleForAdmin.SelectedRoles != null)
                {
                    foreach (var permission in CreateOrEditRoleForAdmin.SelectedRoles)
                    {
                        _permissionService.AddRolePermission(roleId, permission);
                    }
                }
            }


            return RedirectToPage("index");
        }
    }
}

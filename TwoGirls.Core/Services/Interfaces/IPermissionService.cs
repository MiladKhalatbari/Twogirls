using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Role
        public ICollection<Role> GetAllRoles();
        public ICollection<Role> GetAllDeletedRoles();
        public int AddRole(Role role);
        public bool EditRole(Role role);
        public Role GetRoleById(int id);
        public Role GetRoleByIdIgnorequeryFilter(int id);
        public bool DeleteRole(int id);
        public bool RecoverRole(int id);
        #endregion


        #region Permission
        public ICollection<Permission> GetAllPermissions();
        #endregion


        #region RolePermission
        public int AddRolePermission(int roleId, int permissionId);
        public void RemoveExistingPermission(int roleId);
        public bool PermissionCheker(int userId, int permissionId);
        #endregion

    }
}

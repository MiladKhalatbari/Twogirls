using Microsoft.EntityFrameworkCore;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Context;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly TwogirsContext _context;
        public PermissionService(TwogirsContext context)
        {
            _context = context;
        }

        #region Role
        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public bool DeleteRole(int id)
        {
            try
            {
                var role = GetRoleById(id);
                role.IsDelete = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool RecoverRole(int id)
        {
            try
            {
                var role = GetRoleByIdIgnorequeryFilter(id);
                role.IsDelete = false;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool EditRole(Role role)
        {
            try
            {
                _context.Roles.Update(role);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public ICollection<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public Role GetRoleById(int id)
        {
            return _context.Roles.Include(x => x.RolePermissions).FirstOrDefault(x => x.RoleId == id);
        }

        public ICollection<Role> GetAllDeletedRoles()
        {
            return _context.Roles.IgnoreQueryFilters().Where(x => x.IsDelete).ToList();
        }

        public Role GetRoleByIdIgnorequeryFilter(int id)
        {
            return _context.Roles.IgnoreQueryFilters().First(x => x.RoleId == id);
        }
        #endregion




        #region Permission

        public ICollection<Permission> GetAllPermissions()
        {
            return _context.Permissions.ToList();
        }
        #endregion


        #region RolePermission
        public int AddRolePermission(int roleId, int permissionId)
        {
            var rolepermission = new RolePermission()
            {
                RoleId = roleId,
                PermissionId = permissionId
            };
            _context.RolePermissions.Add(rolepermission);
            _context.SaveChanges();
            return rolepermission.Id;
        }
        public void RemoveExistingPermission(int roleId)
        {
            var rolePermissions = _context.RolePermissions.Where(X => X.RoleId == roleId).ToList();
            _context.RemoveRange(rolePermissions);
            _context.SaveChanges();
        }

        public bool PermissionCheker(int userId, int permissionId)
        {
            try
            {
                var roleId = _context.Users.Find(userId).RoleId;
                return _context.RolePermissions.Any(rp => rp.PermissionId == permissionId && rp.RoleId == roleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

    }
}

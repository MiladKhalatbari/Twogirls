using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoGirls.DataLayer.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        [MaxLength(200)]
        [Required]
        public string Title { get; set; }
        public int? ParentId { get; set; }

        #region Relations
        [ForeignKey("ParentId")]
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}

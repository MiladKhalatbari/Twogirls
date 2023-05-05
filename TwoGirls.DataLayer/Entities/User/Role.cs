using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoGirls.DataLayer.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [MaxLength(200)]
        [Required]
        [Display(Name ="")]
        public string RoleTitle { get; set; }

        #region Relation
        public ICollection<Role> Roles { get; set;}
        #endregion
    }
}

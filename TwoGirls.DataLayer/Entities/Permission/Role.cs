using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [MaxLength(200)]
        [Required]
        [Display(Name = "Title")]
        public string RoleTitle { get; set; }
        public bool IsDelete { get; set; }
        #region Relation
        public ICollection<User>? Users { get; set; }
        public ICollection<RolePermission>? RolePermissions { get; set; }

        #endregion
    }
}

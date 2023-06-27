using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public bool IsDelete { get; set; }
        #region Relation
        public ICollection<CategoryToProduct>? Categories { get; set; }
        #endregion
    }
}

using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class Category
    {
        [Required]
        public int  Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        #region Relation
        public ICollection<CategoryToProduct>? Categories { get; set;}
        #endregion
    }
}

using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class ProductType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        #region Relation
        public ICollection<Product>? Products { get; set; }
        #endregion
    }
}

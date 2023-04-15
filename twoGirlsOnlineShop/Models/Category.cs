using System.ComponentModel.DataAnnotations;

namespace twoGirlsOnlineShop.Models
{
    public class Category
    {
        [Required]
        public int  Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<CategoryToProduct>? Categories { get; set;}
    }
}

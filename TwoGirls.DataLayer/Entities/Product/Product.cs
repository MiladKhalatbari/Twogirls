using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal SalesPrice { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal DiscountedPrice { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        public double Rate()
        {
            if (Reviews != null)
            {
                return Reviews.Average(x => x.Rate);
            }
            else
            {
                return 5;
            }
        }

        #region Relation
        public ICollection<ImagePath>? ImagePaths { get; set; }

        public ICollection<CategoryToProduct>? categoryToProdycts { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public List<CardItem>? CardItems { get; set; }
        #endregion
    }
}
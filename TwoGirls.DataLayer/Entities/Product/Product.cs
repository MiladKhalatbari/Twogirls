using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ItemNumber { get; set; }
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
        public bool IsDelete { get; set; }
        public double Rate()
        {

            if (Reviews != null && Reviews.Count != 0)
            {
                return Reviews.Average(x => x.Rate);
            }
            else
            {
                return 5;
            }


        }
        public int ProductTypeId { get; set; }
        #region Relation
        public ICollection<ImagePath>? ImagePaths { get; set; }

        public ICollection<CategoryToProduct>? categoryToProdycts { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public List<CartItem>? CardItems { get; set; }
        public ProductType? ProductType { get; set; }
        #endregion
    }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.DTOs
{
    public class AddOrEditProductViewModel
    {
        [Key]
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
        [Required(ErrorMessage = "Please select files")]
        public List<IFormFile>? Files { get; set; }
        public ICollection<Review>? Reviews { get; set; }

        public List<int>? SelectedCategories { get; set; }
        public List<Category>? Categories { get; set; }

    }
}

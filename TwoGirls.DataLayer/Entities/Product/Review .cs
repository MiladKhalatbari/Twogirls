using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using System.ComponentModel.DataAnnotations;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.DataLayer.Entities
{
    public class Review
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Write a review before submitting your rating.")]
        [MaxLength(400)]
        public string Comment { get; set; }
        [Range(0.5, 5)]
        [Required]
        public Double Rate { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        #region Relation
        public Product? Product { get; set; }
        public User? User { get; set; }
        #endregion

    }
}

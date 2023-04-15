using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using System.ComponentModel.DataAnnotations;

namespace twoGirlsOnlineShop.Models
{
    public class Review
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(400)]
        public string Comment { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
       
    }
}

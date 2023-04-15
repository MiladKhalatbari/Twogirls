using System.ComponentModel.DataAnnotations;

namespace twoGirlsOnlineShop.Models
{
    public class Address
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(64)]
        [Required]
        public string Country { get; set; }
        [MaxLength(64)]
        [Required]
        public string City { get; set; }
        [MaxLength(64)]
        [Required]
        public string Street { get; set; }
        [MaxLength(16)]
        [Required]
        public string BuildingNumber { get; set; }
        [MaxLength(16)]
        public string? UnitNumber { get; set; }
        [MaxLength(16)]
        [Required]
        public string PostCode { get; set; }

        public string? location { get; set; }

        public User User { get; set; }

    }
}

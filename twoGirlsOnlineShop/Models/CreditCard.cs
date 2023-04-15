using System.ComponentModel.DataAnnotations;

namespace twoGirlsOnlineShop.Models
{
    public class CreditCard
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(19)]
        [MinLength(8)]
        public int CreditCardNumber { get; set; }
        [Required]
        [MaxLength(2)]
        [MinLength(2)]
        public int Year { get; set; }
        [Required]
        [MaxLength(2)]
        [MinLength(2)]
        public int Month { get; set; }
        [Required]
        [MaxLength(4)]
        [MinLength(3)]
        public int CVV2 { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace twoGirlsOnlineShop.Models
{
    public class User
    {
       
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(64)]
        public string lastName { get; set; }
        [Required]
        [MaxLength(64)]
        public string Email { get; set; }
        [MaxLength(16)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(64)]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MaxLength(64)]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool IsAdmin { get; set; }


        public ImagePath? ImagePath { get; set; }
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<CreditCard>? CreditCards { get; set; }
        public ICollection<Card>? Cards { get; set; }
        public ICollection<Product>? Favorites { get; set; }
        public ICollection<Review>? Reviews { get; set; }

    }
}

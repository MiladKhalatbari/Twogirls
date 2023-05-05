using System.ComponentModel.DataAnnotations;
namespace TwoGirls.DataLayer.Entities
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
        public string LastName { get; set; }
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(16)]
        public string? PhoneNumber { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [MaxLength(200)]
        public string? ImagePath { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [Required]
        [MaxLength(50)]
        public string ActiveCode { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }

        #region Relation
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<CreditCard>? CreditCards { get; set; }
        public ICollection<Card>? Cards { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public List<Transaction> Transactions { get; set; }

        #endregion

    }
}

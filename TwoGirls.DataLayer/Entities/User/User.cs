using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [MaxLength(50)]
        public string? ActiveCode { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        public int RoleId { get; set; }
        public bool IsDelete { get; set; }

        #region Relation
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<CreditCard>? CreditCards { get; set; }
        public ICollection<Cart>? Cards { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<UserDiscountCodes>? UserDiscountCodes { get; set; }
        [ForeignKey("RoleId")]
        public Role? UserRole { get; set; }
        #endregion

    }
}

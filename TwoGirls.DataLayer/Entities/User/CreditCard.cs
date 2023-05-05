using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwoGirls.DataLayer.Entities
{
    public class CreditCard
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string OwnerName { get; set; }

        [Required]
        [CreditCard(ErrorMessage = "Please enter a valid credit card number")]
        public string CreditCardNumber { get; set; }

        [Required]
        [Range(2023, 2100, ErrorMessage = "Please enter a valid year")]
        public int Year { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Please enter a valid month")]
        public int Month { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "Please enter a valid CVV code")]
        public string CVV { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        #region Relation

        public User? User { get; set; }

        #endregion
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwoGirls.DataLayer.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public bool IsPosted { get; set; }
        public int? AddressId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public decimal OrderPrice { get; set; }
        [Required]
        public DateTime PostDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsFinally { get; set; }
        #region Relation
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("AddressId")]
        public Address? Address { get; set; }
        [ForeignKey("CartId")]
        public Cart? Cart { get; set; }
        #endregion
    }
}

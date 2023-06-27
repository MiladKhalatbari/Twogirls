using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class DiscountCode
    {
        [Key]
        public int DiscountId { get; set; }
        public int? UseableCount { get; set; } = null;
        [Required]
        [MaxLength(255)]
        public string Code { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EendDate { get; set; }
        public bool IsDelete { get; set; }
        #region Relation
        public ICollection<UserDiscountCodes>? UserDiscountCodes { get; set; }
        #endregion
    }
}

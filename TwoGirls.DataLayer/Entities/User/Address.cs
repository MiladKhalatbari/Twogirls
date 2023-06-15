using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [MaxLength(64)]
        [Required]
        public string Country { get; set; }
        [MaxLength(300)]
        public string? Description { get; set; }
        [MaxLength(64)]
        [Required]
        public string Name { get; set; }
        [MaxLength(64)]
        [Required]
        public string City { get; set; }
        [MaxLength(64)]
        [Required]
        public string Street { get; set; }
        [MaxLength(16)]
        public string? BuildingNumber { get; set; }
        [MaxLength(16)]
        public string? UnitNumber { get; set; }
        [MaxLength(16)]
        [Required]
        public string PostCode { get; set; }
        public string? location { get; set; }
        public int UserId { get; set; }

        #region Relation
        public User? User { get; set; }
        public ICollection<Order>? Orders { get; set; }
        #endregion

    }
}

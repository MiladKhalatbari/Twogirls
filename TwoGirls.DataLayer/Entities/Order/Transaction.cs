using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwoGirls.DataLayer.Entities
{
    public class Transaction
    {
        public Transaction()
        {

        }
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public bool Finaly { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }
        public bool IsDelete { get; set; }

        #region Relation
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        [ForeignKey("TypeId")]
        public virtual TransactionType? TransactionType { get; set; }
        #endregion
    }
}

using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class TransactionType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        [Required]
        public string Title { get; set; }

        #region Relation
        public virtual List<Transaction>? Transactions { get; set; }
        #endregion
    }
}

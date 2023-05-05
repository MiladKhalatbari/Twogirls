using System.ComponentModel.DataAnnotations.Schema;

namespace TwoGirls.DataLayer.Entities

{
    public class Favorite
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        #region Relation
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("UserId")]
        public User Users { get; set; }
        #endregion
    }
}

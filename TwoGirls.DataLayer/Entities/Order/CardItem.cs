using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class CardItem
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int CardId { get; set; }
        public int ProductId { get; set; }

        public decimal TotalPrice()
        {
           return Product.DiscountedPrice * Quantity;
        }

        #region Relation
        public Card Card { get; set; }
        public Product Product { get; set; }
        #endregion

    }
}

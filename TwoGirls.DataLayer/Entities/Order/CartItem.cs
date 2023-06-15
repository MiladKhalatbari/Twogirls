using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class CartItem
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }

        public decimal TotalPrice()
        {
           return Product.DiscountedPrice * Quantity;
        }

        #region Relation
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        #endregion

    }
}

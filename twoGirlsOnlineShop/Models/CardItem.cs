using System.ComponentModel.DataAnnotations;

namespace twoGirlsOnlineShop.Models
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
        public Card Card { get; set; }
        public Product Product { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace TwoGirls.DataLayer.Entities
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        [Required]
        public int Id { get; set; }
        public bool IsClose { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TotalOrderPrice()
        {
            decimal total = 0;
            foreach (var item in CartItems)
            {
                total += item.TotalPrice();
            }
            return total;
        }
        public void AddCardItem(Product product)
        {
            if (CartItems.Exists(i => i.Product.Id == product.Id))
            {
                CartItems.Find(i => i.Product.Id == product.Id).Quantity += 1;
            }
            else
            {
                CartItems.Add(new CartItem()
                {
                    CartId = Id,
                    ProductId = product.Id,
                    Quantity = 1
                });
            }
        }
        public void RemoveCardItem(Product product)
        {
            if (CartItems.Exists(i => i.Product.Id == product.Id))
            {
                var cardItem = CartItems.Find(i => i.Product.Id == product.Id);
                if (cardItem.Quantity > 1)
                {
                    cardItem.Quantity -= 1;
                }
                else
                {
                    CartItems.Remove(cardItem);
                }
            }
        }

        #region Relation
        public List<CartItem>? CartItems { get; set; }
        public User? User { get; set; }
        public Order? Order { get; set; }
        #endregion
    }

}

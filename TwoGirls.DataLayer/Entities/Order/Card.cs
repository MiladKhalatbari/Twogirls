using Microsoft.Build.Framework;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.DataLayer.Entities
{
    public class Card
    {
        [Required]
        public int Id { get; set; }
        public bool IsClose { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }

        public decimal TotalOrderPrice()
        {
            decimal total = 0;
            foreach (var item in CardItems)
            {
                total += item.TotalPrice();
            }
            return total;
        }
        public void AddCardItem( Product product)
        {
            if (CardItems.Exists(i => i.Product.Id == product.Id))
            {
                CardItems.Find(i => i.Product.Id == product.Id).Quantity += 1;
            }
            else
            {
                CardItems.Add(new CardItem()
                {
                    CardId = Id,
                    ProductId = product.Id,
                    Quantity = 1
                });
            }
        }
        public void RemoveCardItem(Product product)
        {
            if (CardItems.Exists(i => i.Product.Id == product.Id))
            { var cardItem = CardItems.Find(i => i.Product.Id == product.Id);
                if (cardItem.Quantity > 1)
                {
                    cardItem.Quantity -= 1;
                }
                else 
                {
                 CardItems.Remove(cardItem); 
                }
            }
        }

        #region Relation
        public List<CardItem> CardItems { get; set; }
        public User User { get; set; }
        #endregion
    }

}

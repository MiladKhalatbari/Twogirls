using Microsoft.Build.Framework;

namespace twoGirlsOnlineShop.Models
{
    public class CategoryToProduct
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public Category Category { get; set; }

    }
}

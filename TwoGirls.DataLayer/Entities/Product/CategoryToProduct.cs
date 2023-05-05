using Microsoft.Build.Framework;

namespace TwoGirls.DataLayer.Entities
{
    public class CategoryToProduct
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }


        #region Relation
        public Product Product { get; set; }
        public Category Category { get; set; }
        #endregion

    }
}

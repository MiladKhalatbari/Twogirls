using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoGirls.Core.DTOs;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.Services.Interfaces
{
    public interface IProductService
    {
        #region Product
        public FilterProductViewModel GetProductsByFilter(string filter, int pageId, int startPrice, int endPrice, int prodcutTypeId, string orderBY, List<int>? selectedCategories);
        public FavoriteProductViewModel GetFavoriteProductsByFilter(int userId, string filter, int pageId, int startPrice, int endPrice, string orderBY);
        public ProductsForAdminViewModel GetProductsByFilterForAdminViewModel(int pageId,string filter);
        public ProductsForAdminViewModel GetDeletedProductsByFilterForAdminViewModel(int pageId, string filter);
        public int AddProduct(Product role);
        public bool EditProduct(Product role);
        public bool DeleteProduct(int id);
        public bool RecoverProduct(int id);
        public Product GetProductByIdIgnorequeryFilterIncludeImage(int id);
        public Product GetProductByIdIncludeImage(int id);
        public Dictionary<int, int> GetCategoryCounts(List<Category> categories, List<CategoryToProduct>? categoryToProducts, int prodcutTypeId);
        public int ProductMaxPrice();
        public int ProductMinPrice();
        #endregion

        #region ImagePath        
        public bool AddImagePath(ImagePath imagePath);
        public bool TemporaryDeleteImagePath(ImagePath imagePath);
        public ImagePath GetImagePathById(int id);
        #endregion

        #region Category
        public List<Category> GetAllCategories();
        public List<int> GetAllCategoriesOfProduct(int id);
        public bool AddCategoryToProduct(int productId, int categoryId);
        public void RemoveExistingProductCategories(int productId);
        public bool AddCategory(Category category);
        public bool DeleteCategory(int id);
        #endregion

        #region ProductType
        public List<ProductType> GetAllProductType();
        #endregion

    }
}

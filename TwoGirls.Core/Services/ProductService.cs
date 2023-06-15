using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Context;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.Services
{
    public class ProductService : IProductService
    {
        TwogirsContext _context;
        public ProductService(TwogirsContext context)
        {
            _context = context;
        }

        #region Product   

        public int AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.Id;
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = GetProductByIdIncludeImage(id);
                product.IsDelete = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }


        }

        public bool EditProduct(Product product)
        {
            try
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
        public bool RecoverProduct(int id)
        {
            try
            {
                var product = GetProductByIdIgnorequeryFilterIncludeImage(id);
                product.IsDelete = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public Product GetProductByIdIgnorequeryFilterIncludeImage(int id)
        {
            return _context.Products.IgnoreQueryFilters().Include(x => x.ImagePaths).FirstOrDefault(p => p.Id == id);
        }

        public Product GetProductByIdIncludeImage(int id)
        {
            return _context.Products.Include(x => x.ImagePaths).FirstOrDefault(p => p.Id == id);
        }

        public ProductsForAdminViewModel GetProductsByFilterForAdminViewModel(int pageId, string filter)
        {
            IQueryable<Product> result = _context.Products;


            if (!filter.IsNullOrEmpty())
            {
                result = result.Where(x => x.Title.ToLower().Contains(filter.ToLower()) || (x.Description != null && x.Description.ToLower().Contains(filter.ToLower())));
            }

            int take = 20;
            int skip = (pageId - 1) * take;
            ProductsForAdminViewModel list = new ProductsForAdminViewModel()
            {
                CurrentPage = pageId,
                PageCount = (int)Math.Ceiling(result.Count() / (double)take),
                Products = result.OrderBy(u => u.ReleaseDate).Skip(skip).Take(take).Include(x => x.ImagePaths).ToList()
            };
            return list;
        }
        public ProductsForAdminViewModel GetDeletedProductsByFilterForAdminViewModel(int pageId, string filter)
        {
            IQueryable<Product> result = _context.Products.IgnoreQueryFilters().Where(x => x.IsDelete);


            if (!filter.IsNullOrEmpty())
            {
                result = result.Where(x => x.Title.ToLower().Contains(filter.ToLower()) || (x.Description != null && x.Description.ToLower().Contains(filter.ToLower())));
            }

            int take = 20;
            int skip = (pageId - 1) * take;
            ProductsForAdminViewModel list = new ProductsForAdminViewModel()
            {
                CurrentPage = pageId,
                PageCount = (int)Math.Ceiling(result.Count() / (double)take),
                Products = result.OrderBy(u => u.ReleaseDate).Skip(skip).Take(take).Include(x => x.ImagePaths).ToList()
            };
            return list;
        }

        public FilterProductViewModel GetProductsByFilter(string filter, int pageId, int startPrice, int endPrice, int prodcutTypeId, string orderBY, List<int>? selectedCategories)
        {
            IEnumerable<Product> result = _context.Products.Include(x => x.ImagePaths).Include(x => x.categoryToProdycts).Include(x => x.Reviews);
            
            if (prodcutTypeId != 0) 
            {
                result = result.Where(x => x.ProductTypeId == prodcutTypeId); 
            }
                  
            if (!filter.IsNullOrEmpty())
            {
                result = result.Where(x => x.Title.ToLower().Contains(filter.ToLower()) || (x.Description != null && x.Description.ToLower().Contains(filter.ToLower())));
            }

            if (!selectedCategories.IsNullOrEmpty())
            {
                foreach (var categoryId in selectedCategories)
                {
                    result = result.Where(x => x.categoryToProdycts.Any(x => x.CategoryId == categoryId));

                }
            }
            if (endPrice != 0)
            {
                result = result.Where(x => x.DiscountedPrice <= endPrice);

            }
            if (startPrice != 0)
            {
                result = result.Where(x => x.DiscountedPrice >= startPrice);
            }
            switch (orderBY)
            {
                case "best":
                    result = result.OrderByDescending(x => x.CardItems.Count());
                    break;
                case "arrivals":
                    result = result.OrderByDescending(x => x.PurchaseDate);
                    break;
                case "top_rated":
                    result = result.OrderByDescending(x => x.Rate());
                    break;
                case "high":
                    result = result.ToList().OrderByDescending(x => x.DiscountedPrice);
                    break;
                case "low":
                    result = result.OrderBy(x => x.DiscountedPrice);
                    break;

                default:
                    break;
            }
            List<CategoryToProduct> categoryToProducts = result.SelectMany(p => p.categoryToProdycts).Distinct().ToList();

            int take = 1;
            int skip = (pageId - 1) * take;
            FilterProductViewModel filterViewModel = new FilterProductViewModel();
            filterViewModel.PaginationViewModel = new PaginationViewModel()
            {
                CurrentPage = pageId,
                PageCount = (int)Math.Ceiling(result.AsEnumerable().Count() / (double)take)
            };
            filterViewModel.ProdcutTypeId = prodcutTypeId;
            filterViewModel.Products = result.Skip(skip).Take(take).ToList();
            filterViewModel.Categories = _context.Categories.ToList();
            filterViewModel.MinPrice = ProductMinPrice();
            filterViewModel.MaxPrice = ProductMaxPrice();
            filterViewModel.ProdcutTypes = GetAllProductType();
            filterViewModel.categoryToProdycts = categoryToProducts;
            filterViewModel.SelectedCategories = selectedCategories;
            return filterViewModel;
        }

        public FavoriteProductViewModel GetFavoriteProductsByFilter(int userId, string filter, int pageId, int startPrice, int endPrice, string orderBY)
        {
            IEnumerable<Product> result = _context.Favorites.Where(f => f.UserId == userId).Include(f => f.Product.ImagePaths).Include(f => f.Product.CardItems).Include(f => f.Product.Reviews).Select(f => f.Product);


            if (!filter.IsNullOrEmpty())
            {
                result = result.Where(x => x.Title.ToLower().Contains(filter.ToLower()) || (x.Description != null && x.Description.ToLower().Contains(filter.ToLower())));
            }

            if (endPrice != 0)
            {
                result = result.Where(x => x.DiscountedPrice <= endPrice);

            }
            if (startPrice != 0)
            {
                result = result.Where(x => x.DiscountedPrice >= startPrice);
            }
            switch (orderBY)
            {
                case "best":
                    result = result.OrderByDescending(x => x.CardItems.Count());
                    break;
                case "arrivals":
                    result = result.OrderByDescending(x => x.PurchaseDate);
                    break;
                case "top_rated":
                    result = result.OrderByDescending(x => x.Rate());
                    break;
                case "high":
                    result = result.ToList().OrderByDescending(x => x.DiscountedPrice);
                    break;
                case "low":
                    result = result.OrderBy(x => x.DiscountedPrice);
                    break;

                default:
                    break;
            }
            int take = 1;
            int skip = (pageId - 1) * take;
            FavoriteProductViewModel FavoriteProductModel = new FavoriteProductViewModel();
            FavoriteProductModel.Products = result.Skip(skip).Take(take).ToList();
            FavoriteProductModel.MinPrice = ProductMinPrice();
            FavoriteProductModel.MaxPrice = ProductMaxPrice();
            FavoriteProductModel.PaginationViewModel = new PaginationViewModel()
            {
                CurrentPage = pageId,
                PageCount = (int)Math.Ceiling(result.AsEnumerable().Count() / (double)take)
            };
            return FavoriteProductModel;
        }

        public int ProductMaxPrice()
        {
            return Convert.ToInt32(_context.Products.Max(x => x.DiscountedPrice));
        }
        public int ProductMinPrice()
        {
            return Convert.ToInt32(_context.Products.Min(x => x.DiscountedPrice));
        }

        public Dictionary<int, int> GetCategoryCounts(List<Category> categories, List<CategoryToProduct>? categoryToProducts, int prodcutTypeId)
        {
            Dictionary<int, int> categoryCounts = new Dictionary<int, int>();
            foreach (var category in categories.Where(x => x.ParentId == prodcutTypeId))
            {

                foreach (var subcategory in categories.Where(x => x.ParentId == category.Id))
                {
                    categoryCounts.Add(subcategory.Id, categoryToProducts.Count(cp => cp.CategoryId == subcategory.Id));


                }

            }
            return categoryCounts;

        }
        #endregion

        #region ImagePath
        public bool AddImagePath(ImagePath imagePath)
        {
            try
            {
                _context.ImagePaths.Add(imagePath);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
        public ImagePath GetImagePathById(int id)
        {
            return _context.ImagePaths.FirstOrDefault(x => x.Id == id);
        }
        public bool TemporaryDeleteImagePath(ImagePath imagePath)
        {
            try
            {
                _context.ImagePaths.Remove(imagePath);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
        #endregion

        #region Category
        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
        public List<int> GetAllCategoriesOfProduct(int id)
        {
            return _context.CategoryToProducts.Where(x => x.ProductId == id).Select(x => x.CategoryId).ToList();
        }
        public void RemoveExistingProductCategories(int productId)
        {
            var categoryToProducts = _context.CategoryToProducts.Where(x => x.ProductId == productId).ToList();
            _context.CategoryToProducts.RemoveRange(categoryToProducts);
            _context.SaveChanges();
        }
        public bool AddCategoryToProduct(int productId, int categoryId)
        {
            try
            {
                var itemCagegory = new CategoryToProduct()
                {
                    ProductId = productId,
                    CategoryId = categoryId
                };
                _context.CategoryToProducts.Add(itemCagegory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }

        }

        public bool AddCategory(Category category)
        {

            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool DeleteCategory(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        #endregion

        #region ProductType
        public List<ProductType> GetAllProductType()
        {
          return _context.productTypes.ToList();
        }
        #endregion


    }
}

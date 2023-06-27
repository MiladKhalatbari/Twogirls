using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Context;
using TwoGirls.DataLayer.Entities;
using Product = TwoGirls.DataLayer.Entities.Product;
using Review = TwoGirls.DataLayer.Entities.Review;

namespace TwoGirls.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly TwogirsContext _context;
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

            var take = 20;
            var skip = (pageId - 1) * take;
            var list = new ProductsForAdminViewModel()
            {
                CurrentPage = pageId,
                PageCount = (int)Math.Ceiling(result.Count() / (double)take),
                Products = result.OrderBy(u => u.ReleaseDate).Skip(skip).Take(take).Include(x => x.ImagePaths).ToList()
            };
            return list;
        }
        public ProductsForAdminViewModel GetDeletedProductsByFilterForAdminViewModel(int pageId, string filter)
        {
            var result = _context.Products.IgnoreQueryFilters().Where(x => x.IsDelete);


            if (!filter.IsNullOrEmpty())
            {
                result = result.Where(x => x.Title.ToLower().Contains(filter.ToLower()) || (x.Description != null && x.Description.ToLower().Contains(filter.ToLower())));
            }

            var take = 20;
            var skip = (pageId - 1) * take;
            var list = new ProductsForAdminViewModel()
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
                    result = result.OrderByDescending(x => x.CardItems?.Count());
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
            var categoryToProducts = result.SelectMany(p => p.categoryToProdycts).Distinct().ToList();

            var take = 1;
            var skip = (pageId - 1) * take;
            var filterViewModel = new FilterProductViewModel
            {
                PaginationViewModel = new PaginationViewModel()
                {
                    CurrentPage = pageId,
                    PageCount = (int)Math.Ceiling(result.AsEnumerable().Count() / (double)take)
                },
                ProdcutTypeId = prodcutTypeId,
                Products = result.Skip(skip).Take(take).ToList(),
                Categories = _context.Categories.ToList(),
                MinPrice = ProductMinPrice(),
                MaxPrice = ProductMaxPrice(),
                ProdcutTypes = GetAllProductType(),
                categoryToProdycts = categoryToProducts,
                SelectedCategories = selectedCategories
            };
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
            var take = 1;
            var skip = (pageId - 1) * take;
            var FavoriteProductModel = new FavoriteProductViewModel
            {
                Products = result.Skip(skip).Take(take).ToList(),
                MinPrice = ProductMinPrice(),
                MaxPrice = ProductMaxPrice(),
                PaginationViewModel = new PaginationViewModel()
                {
                    CurrentPage = pageId,
                    PageCount = (int)Math.Ceiling(result.AsEnumerable().Count() / (double)take)
                }
            };
            return FavoriteProductModel;
        }
        public DetailProductViewModel GetProductAndReviewForDetailPageByFilter(int productId, int starAmount, int pageId)
        {
            var reviews = _context.Review.IgnoreQueryFilters().Include(r => r.User).Where(r => r.ProductId == productId);
            if (starAmount != 0)
            {
                reviews = reviews.Where(r => r.Rate == starAmount);
            }
            var take = 1;
            var skip = (pageId - 1) * take;
            var DetailProductModel = new DetailProductViewModel()
            {
                Product = GetProductByIdIncludeImage(productId),
                PaginationViewModel = new PaginationViewModel()
                {
                    CurrentPage = pageId,
                    PageCount = (int)Math.Ceiling(reviews.AsEnumerable().Count() / (double)take)
                },
                ProductId = productId,
                Reviews = reviews.Skip(skip).Take(take).ToList(),
                SelectedStarAmount = starAmount
            };
            return DetailProductModel;

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
            var categoryCounts = new Dictionary<int, int>();
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
        public List<Category> GetDeletedCategoriesIgnoreQueryFilters()
        {
            return _context.Categories.IgnoreQueryFilters().Where(c => c.IsDelete).ToList();
        }
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
        public bool UpdateCategory(Category category)
        {
            try
            {
                _context.Categories.Update(category);
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
                var category = GetCategoryById(id);
                if (category != null)
                {
                    category.IsDelete = true;
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
        public bool RecoverCategory(int id)
        {
            try
            {
                var category = GetCategoryByIdIgnoreQueryFilters(id);
                if (category != null)
                {
                    category.IsDelete = false;
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
        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
        public Category GetCategoryByIdIgnoreQueryFilters(int id)
        {
            return _context.Categories.IgnoreQueryFilters().FirstOrDefault(c => c.Id == id);
        }
        #endregion

        #region ProductType
        public List<ProductType> GetAllProductType()
        {
            return _context.productTypes.ToList();
        }
        #endregion

        #region Reviews
        public int AddReview(Review review)
        {
            _context.Review.Add(review);
            _context.SaveChanges();
            return review.Id;
        }
        public List<Review> GetAllReviews(int productId)
        {
            return _context.Review.Where(r => r.ProductId == productId).ToList();
        }
        #endregion

    }
}
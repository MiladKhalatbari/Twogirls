using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using TwoGirls.Core.Convertors;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Context;
using TwoGirls.DataLayer.Entities;
using Review = TwoGirls.DataLayer.Entities.Review;

namespace twoGirlsOnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        public ProductController(TwogirsContext myContext, IProductService productService, IUserService userService)
        {
            _userService = userService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Detail Product

        public IActionResult Detail(int id = 0, int starAmount = 0, int pageId = 1)
        {
            var model = _productService.GetProductAndReviewForDetailPageByFilter(id, starAmount, pageId);
            if (int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                ViewData["Favorites"] = _userService.GetUserFavorites(userId);
            }
            ViewData["Reviews"] = _productService.GetAllReviews(id);
            if (model.Product == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult AjaxGetReviews(int productId = 0, int starAmount = 0, int pageId = 1)
        {
            var model = _productService.GetProductAndReviewForDetailPageByFilter(productId, starAmount, pageId);
            var reviewListPartialView = RenderViewToString.RenderRazorViewToString(this, "Product/_ReviewsBox", model);
            var paginationPartialView = RenderViewToString.RenderRazorViewToString(this, "Product/_Pagination", model.PaginationViewModel);
            return Json(new { success = true, reviewListPartialView, paginationPartialView });
        }

        #endregion

        #region Filter Product

        [HttpGet]
        public IActionResult Filter(string filter = "", int pageId = 1, int startPrice = 0, int endPrice = 0, int prodcutTypeId = 0, string orderBY = "", List<int>? selectedCategories = null)
        {

            var model = _productService.GetProductsByFilter(filter, pageId, startPrice, endPrice, prodcutTypeId, orderBY, selectedCategories);

            return View(model);
        }

        [HttpGet]
        public IActionResult AjaxFilter(string filter = "", int pageId = 1, int startPrice = 0, int endPrice = 0, int prodcutTypeId = 0, string orderBY = "", List<int>? selectedCategories = null)
        {

            var model = _productService.GetProductsByFilter(filter, pageId, startPrice, endPrice, prodcutTypeId, orderBY, selectedCategories);
            var productListPartialView = RenderViewToString.RenderRazorViewToString(this, "Product/_ProductsBox", model.Products);
            var paginationPartialView = RenderViewToString.RenderRazorViewToString(this, "Product/_Pagination", model.PaginationViewModel);
            var categorySelectionPartialView = RenderViewToString.RenderRazorViewToString(this, "Product/_CategorySelection", model);

            return Json(new { success = true, productListPartialView, paginationPartialView, categorySelectionPartialView });

        }
        #endregion

        #region Add Review
        [HttpGet]
        public IActionResult AddReview(int productId, double rating)
        {

            if (int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                var review = new Review()
                {
                    ProductId = productId,
                    UserId = userId,
                    Rate = rating
                };
                return View(review);
            }
            else
            {
                return PartialView("Product/_LoginModalForm");
            }
        }

        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            if (ModelState.IsValid)
            {
                review.Date = DateTime.Now;
                _productService.AddReview(review);
                return Json(new { success = true });
            }
            else
            {
                var partialView = RenderViewToString.RenderRazorViewToString(this, "AddReview", review);
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, errors = errors, html = partialView });
            }
        }
        #endregion

        #region Favorite Product
        public IActionResult AddFavorite(int id)
        {
            if (int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {

                var product = _productService.GetProductByIdIncludeImage(id);
                if (product == null)
                {
                    return NotFound();
                }
                else if (_userService.IsInUserFavorites(userId, product.Id))
                {
                    _userService.DeleteFavorite(userId, product.Id);
                    return Json(new { isLogin = true });
                }
                else
                {
                    var favorite = new Favorite()
                    {
                        ProductId = id,
                        UserId = userId
                    };
                    _userService.AddFavorite(favorite);
                    return Json(new { isLogin = true });
                }
            }
            else
            {
                var partialView = RenderViewToString.RenderRazorViewToString(this, "Product/_LoginModalForm");
                return Json(new { isLogin = false, html = partialView });
            }

        }
        [Authorize]
        [HttpGet]
        public IActionResult Favorite(string filter = "", int pageId = 1, int startPrice = 0, int endPrice = 0, string orderBY = "")
        {
            if (int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {

                var viewModel = _productService.GetFavoriteProductsByFilter(userId, filter, pageId, startPrice, endPrice, orderBY);
                return View(viewModel);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpGet]
        public IActionResult AjaxFavoriteFilter(string filter = "", int pageId = 1, int startPrice = 0, int endPrice = 0, string orderBY = "")
        {
            if (int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                var model = _productService.GetFavoriteProductsByFilter(userId, filter, pageId, startPrice, endPrice, orderBY);
                var productListPartialView = RenderViewToString.RenderRazorViewToString(this, "Product/_ProductsBox", model.Products);
                var paginationPartialView = RenderViewToString.RenderRazorViewToString(this, "Product/_Pagination", model.PaginationViewModel);
                return Json(new { success = true, productListPartialView, paginationPartialView });
            }
            return BadRequest();
        }
        #endregion

    }
}

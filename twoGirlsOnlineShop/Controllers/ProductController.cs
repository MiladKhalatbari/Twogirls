using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwoGirls.DataLayer.Context;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using TwoGirls.DataLayer.Entities;
using System.Security.Claims;
using twoGirlsOnlineShop.Models;
using TwoGirls.Core.Convertors;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Services.Interfaces;
using System.Numerics;

namespace twoGirlsOnlineShop.Controllers
{
    public class ProductController : Controller
    {
        TwogirsContext _myContext { get; set; }
        private readonly IProductService _productService;

        public ProductController(TwogirsContext myContext, IProductService productService)
        {
            _myContext = myContext;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Detail Product

        public IActionResult Detail(int id)
        {
            var model = _myContext.Products.Include(x=> x.ImagePaths).Include(x=> x.Reviews).ThenInclude(r=> r.User).FirstOrDefault(p=> p.Id == id);
            int userId = int.Parse(HttpContext.User.FindFirstValue(claimType: ClaimTypes.NameIdentifier));
            ViewData["Favorites"] = _myContext.Users.Where(x => x.Id == userId).Include(x => x.Favorites).SelectMany(x => x.Favorites).ToList();
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        #endregion

        #region Filter Product

        [HttpGet]
        public IActionResult Filter(string filter = "",int pageId=1, int startPrice=0, int endPrice = 0, int prodcutTypeId = 0, string orderBY="",List<int>? selectedCategories=null)
        {

            var model = _productService.GetProductsByFilter(filter, pageId, startPrice, endPrice, prodcutTypeId, orderBY, selectedCategories);

            return View(model);
        }
        
        [HttpGet]
        public IActionResult AjaxFilter(string filter = "", int pageId = 1, int startPrice = 0, int endPrice = 0, int prodcutTypeId = 0, string orderBY = "", List<int>? selectedCategories = null)
        {

            var model = _productService.GetProductsByFilter(filter, pageId, startPrice, endPrice, prodcutTypeId, orderBY, selectedCategories);
            var productListPartialView = RenderViewToString.RenderRazorViewToString(this, "FilterProductPage/_ProductsBox", model.Products);
            var paginationPartialView = RenderViewToString.RenderRazorViewToString(this, "FilterProductPage/_Pagination",model.PaginationViewModel);
            var categorySelectionPartialView = RenderViewToString.RenderRazorViewToString(this, "FilterProductPage/_CategorySelection", model);

            return Json(new{ success = true,productListPartialView, paginationPartialView , categorySelectionPartialView });

        }
        #endregion

        #region Add Review
        public IActionResult AddReview(int productId, double rating)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var review = new Review()
            {
                ProductId = productId,
                UserId = userId,
                Rate = rating
            };
            return View(review);
        }

        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            if (ModelState.IsValid)
            {
                review.Date= DateTime.Now;
                _myContext.Review.Add(review);
                _myContext.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                string partialView = RenderViewToString.RenderRazorViewToString(this, "AddReview", review);
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, errors = errors, html = partialView });
            }
        }
        #endregion

        #region Favorite Product
        public IActionResult AddFavorite(int id)
        {
            int userId = int.Parse(User.FindFirstValue(claimType: ClaimTypes.NameIdentifier));
            var user = _myContext.Users.Include(x => x.Favorites).FirstOrDefault(x => x.Id == userId);
            var product = _myContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            else if (user == null)
            {
                return View("Views/Account/Login.cshtml");
            }
            else if (_myContext.Favorites.Any(f => f.UserId == userId && f.ProductId == id))
            {
                var favorite = _myContext.Favorites.FirstOrDefault(f => f.UserId == userId && f.ProductId == id);
                _myContext.Favorites.Remove(favorite);
                _myContext.SaveChanges();
                return Ok();
            }
            else
            {
                var favorite = new Favorite()
                {
                    ProductId = id,
                    UserId = userId
                };
                _myContext.Favorites.Add(favorite);
                _myContext.SaveChanges();
                return Ok();
            }
        }

        [HttpGet]
        public IActionResult Favorite(string filter="", int pageId=1, int startPrice=0, int endPrice=0, string orderBY="")
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            FavoriteProductViewModel viewModel = _productService.GetFavoriteProductsByFilter(userId, filter,pageId,startPrice,endPrice,orderBY);
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult AjaxFavoriteFilter(string filter = "", int pageId = 1, int startPrice = 0, int endPrice = 0, string orderBY = "")
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = _productService.GetFavoriteProductsByFilter(userId,filter, pageId, startPrice, endPrice, orderBY);     
            var productListPartialView = RenderViewToString.RenderRazorViewToString(this, "FilterProductPage/_ProductsBox", model.Products);
            var paginationPartialView = RenderViewToString.RenderRazorViewToString(this, "FilterProductPage/_Pagination", model.PaginationViewModel);
                return Json(new { success = true, productListPartialView, paginationPartialView});
        }
        #endregion
                

    }
}

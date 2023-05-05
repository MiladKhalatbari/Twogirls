using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwoGirls.DataLayer.Context;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using TwoGirls.DataLayer.Entities;
using System.Security.Claims;
using twoGirlsOnlineShop.Models;
using TwoGirls.Core.Convertors;

namespace twoGirlsOnlineShop.Controllers
{
    public class ProductController : Controller
    {
        TwogirsContext _myContext { get; set; }
        private readonly ILogger<HomeController> _logger;

        public ProductController(TwogirsContext myContext, ILogger<HomeController> logger)
        {
            _myContext = myContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id =1)
        {
            var model = _myContext.Products.Find(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [Route("Filter/{id}/{name}")]
        public IActionResult Filter(int id, int name)
        {
            //List<Product> model = new List<Product>();
            if (User.Identity.IsAuthenticated) {
                int userId = int.Parse(User.FindFirstValue(claimType: ClaimTypes.NameIdentifier));
                ViewData["Favorites"] = _myContext.Users.Where(x => x.Id == userId).Include(x=> x.Favorites).SelectMany(x=> x.Favorites).ToList();
            }
            var model = _myContext.Products.
            Include(x => x.ImagePaths).
            Where(p => p.categoryToProdycts.
            Any(c => c.CategoryId == id) && p.categoryToProdycts.
            Any(c => c.CategoryId == name)).
            ToList();

            return View(model);
        }
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

        #region add favorite
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
        public IActionResult Favorite()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           IEnumerable<Product> favorites = _myContext.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Product.ImagePaths)
                .Select(f => f.Product)
                .ToList();

            ViewData["Favorites"] = ViewData["Favorites"] = _myContext.Users.Where(x => x.Id == userId).Include(x => x.Favorites).SelectMany(x => x.Favorites).ToList();
            return View("Filter", favorites);
        }
        #endregion



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

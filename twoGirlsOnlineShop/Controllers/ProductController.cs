using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using twoGirlsOnlineShop.Data;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using twoGirlsOnlineShop.Models;

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
        public IActionResult Detail(int id)
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


           var model = _myContext.Products.
           Include(x => x.ImagePaths).
           Where(p => p.categoryToProdycts.
           Any(c => c.CategoryId == id) && p.categoryToProdycts.
           Any(c => c.CategoryId == name)).
           ToList();

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

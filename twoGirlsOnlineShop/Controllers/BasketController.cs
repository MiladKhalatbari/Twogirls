using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing;
using System.Security.Claims;
using twoGirlsOnlineShop.Data;
using twoGirlsOnlineShop.Models;

namespace twoGirlsOnlineShop.Controllers
{
    public class BasketController : Controller
    {
        private TwogirsContext _myContext;
        private readonly ILogger<HomeController> _logger;
        private User? _user;
        public BasketController(ILogger<HomeController> logger, TwogirsContext myContex)
        {
            _logger = logger;
            _myContext = myContex;
           _user = _myContext.Users.FirstOrDefault(x => x.Id == 1);

        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult AddToCard(int id)
        {
            int userId= Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            var product = _myContext.Products.Find(id);
            var card = _myContext.Cards.Include(x => x.CardItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId && x.IsClose == false);
            if (product != null && card != null)
            {
                card.AddCardItem(product);
                _myContext.Update(card);
                _myContext.SaveChanges();
            }
            return View("Views/Home/Index.cshtml");
        }
        [Authorize]
        public IActionResult RemoveFromCard(int id)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            var product = _myContext.Products.Find(id);
            var card = _myContext.Cards.Include(x => x.CardItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId && x.IsClose==false);
            if (product != null && card != null)
            {
                card.RemoveCardItem(product);
                _myContext.Update(card);
                _myContext.SaveChanges();
            }
            return View("Views/Home/Index.cshtml");
        }
        public IActionResult PaymentAndAddress (int id) 
        {
            var card = _myContext.Cards.Include(x => x.CardItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id);
            if ( card != null)
            {
                card.IsClose = true;
                _myContext.SaveChanges();
            }
                return View("Views/Home/Index.cshtml");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

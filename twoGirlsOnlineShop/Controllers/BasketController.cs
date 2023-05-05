using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Security.Claims;
using TwoGirls.DataLayer.Context;
using TwoGirls.DataLayer.Entities;
using twoGirlsOnlineShop.Models;

namespace twoGirlsOnlineShop.Controllers
{
    public class BasketController : Controller
    {
        private TwogirsContext _myContext;
        private readonly ILogger<HomeController> _logger;
        public BasketController(ILogger<HomeController> logger, TwogirsContext myContex)
        {
            _logger = logger;
            _myContext = myContex;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult AddToCard(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            var product = _myContext.Products.Find(id);
            var card = _myContext.Cards
                .Include(x => x.CardItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.UserId == userId && x.IsClose == false);
            if (card == null)
            {
                card = new Card()
                {
                    IsClose = false,
                    UserId = userId,
                    CreateDate = DateTime.Now
                };

                _myContext.Cards.Add(card);
                _myContext.SaveChanges();
                card = _myContext.Cards.Include(i => i.CardItems).ThenInclude(x => x.Product).ThenInclude(p => p.ImagePaths).FirstOrDefault(x => x.UserId == userId && x.IsClose == false);
            }
            card.AddCardItem(product);
            _myContext.SaveChanges();
            return ViewComponent("CardComponent");
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
            return ViewComponent("CardComponent");
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

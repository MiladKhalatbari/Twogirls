using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using twoGirlsOnlineShop.Data;
using twoGirlsOnlineShop.Models;

namespace twoGirlsOnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private TwogirsContext _myContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, TwogirsContext myContex)
        {
            _logger = logger;
            _myContext = myContex;

           //var _user = _myContext.Users.Include(x=> x.Cards).FirstOrDefault(x => x.Id == 1);
           // if (!_user.Cards.Any(x => x.IsClose == false))
           // {
           //     Card card1 = new Card()
           //     {
           //         IsClose = false,
           //         UserId = _user.Id,
           //        CreateDate=DateTime.Now
           //     };
           //     _user.Cards.Add(card1);
           //     _myContext.SaveChanges();
           // }
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TwoGirls.DataLayer.Context;
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
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("Success")]
        public IActionResult Success()
        {
            return View();
        }
        [Route("Failed")]
        public IActionResult Failed()
        {
            return View();
        }
    }
}
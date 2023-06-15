using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Context;
using twoGirlsOnlineShop.Models;

namespace twoGirlsOnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
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
            //var Transaction = new TwoGirls.DataLayer.Entities.Transaction()
            //{
            //    TypeId = 1,
            //    UserId = userId,
            //    Amount = chargeWallet.Amount,
            //    Date = DateTime.Now,
            //    Description = "Wallet Deposit",
            //    Finaly = true
            //};
            //_userService.AddTransaction(Transaction);
            return View();
        }
        [Route("Failed")]
        public IActionResult Failed()
        {
            return View();
        }
    }
}
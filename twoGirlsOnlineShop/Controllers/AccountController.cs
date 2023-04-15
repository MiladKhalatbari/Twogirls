using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using twoGirlsOnlineShop.Data;
using twoGirlsOnlineShop.Models;

namespace twoGirlsOnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private TwogirsContext _myContext;
        private readonly ILogger<HomeController> _logger;

        public AccountController(ILogger<HomeController> logger, TwogirsContext myContex)
        {
            _logger = logger;
            _myContext = myContex;
        }
            public IActionResult Index()
        {
            return View();
        }
        #region register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserViewModel user)
        {
            if(_myContext.Users.Any(u=> u.Email.ToLower() == user.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "The email you entered already registered. try Again");
                return View();
            }
            if (ModelState.IsValid)
            {
                User newUser = new User()
                {
                    Email = user.Email.ToLower(),
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    ConfirmPassword = user.ConfirmPassword,
                    FirstName = user.FirstName,
                    lastName = user.lastName,
                    IsAdmin = false
                };
                _myContext.Add(newUser);
                _myContext.SaveChanges();
                return RedirectToAction("Login", user);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        #endregion

        #region login
        public IActionResult Login(UserViewModel user)
        {
            return View(user);
        }
        [HttpPost]
        public IActionResult Login(UserViewModel user,int id)
        {
           
            if (_myContext.Users.Any(x => x.Email.ToLower() == user.Email.ToLower()))
            {
                if (_myContext.Users.Any(x => x.Email.ToLower() == user.Email.ToLower() && x.Password == user.Password))
                {
                    var userForCookie = _myContext.Users.First(x => x.Email.ToLower() == user.Email.ToLower() && x.Password == user.Password);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,userForCookie.Id.ToString()),
                        new Claim(ClaimTypes.Email,userForCookie.Email.ToLower()),
                        new Claim(ClaimTypes.Name,userForCookie.FirstName),
                        new Claim(ClaimTypes.Surname,userForCookie.lastName),
                        new Claim("IsAdmin",userForCookie.IsAdmin.ToString())

                    };
                    var identity =new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);  
                    var Properties = new AuthenticationProperties() { IsPersistent = user.RememberMe };
                    HttpContext.SignInAsync(principal, Properties);

                    return Redirect("/");
                }
                ModelState.AddModelError("Password", "The Password you entered is incorrect. try again");
                return View(user);

            };
            ModelState.AddModelError("Email", "The email you entered is incorrect. try again");
            return View(user);
        }
        #endregion

        #region logout
        public IActionResult Logout(UserViewModel user)
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("Login");
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

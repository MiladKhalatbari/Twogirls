using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using TwoGirls.DataLayer.Context;
using TwoGirls.DataLayer.Entities;
using TwoGirls.Core.DTOs;
using twoGirlsOnlineShop.Models;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.Core.Services;
using TwoGirls.Core.Convertors;
using TwoGirls.Core.Security;
using TwoGirls.Core.Generator;
using System;
using TwoGirls.Core.Senders;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Stripe.FinancialConnections;
using Stripe.Checkout;
using Stripe;
using Product = TwoGirls.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.Logging;
using Stripe.Issuing;

namespace twoGirlsOnlineShop.Controllers
{
    public class AccountController : Controller
    {


        private static string s_wasmClientURL = string.Empty;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IRenderViewWithoutControllerToString _renderViewWithoutControllerTo;
        public AccountController(ILogger<HomeController> logger, IUserService userService, IRenderViewWithoutControllerToString renderViewWithoutControllerTo, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _renderViewWithoutControllerTo = renderViewWithoutControllerTo;
            _configuration = configuration;

        }

        #region Edit Profile
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userService.GetUserById(userId);
            ViewBag.WalletAmount = _userService.GetAccountBalance(userId).ToString();
            ViewData["EditProfileViewModel"] = new EditProfileViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
            };
            ViewData["UserAvatarViewModel"] = new UserAvatarViewModel()
            {
                ImagePath = user.ImagePath,
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email
            };
            ViewData["ChangePasswordViewModel"] = new ChangePasswordViewModel()
            {
                Id = userId
            };
            ViewData["AddUserCreditCardViewModel"] = new CreditCard()
            {
                UserId = userId
            };
            ViewData["AddUserAddressViewModel"] = new TwoGirls.DataLayer.Entities.Address()
            {
                UserId = userId
            };
            ViewData["UserAddressesViewModel"] = _userService.GetUserAddresses(userId);
            ViewData["UserCreditCardsViewModel"] = _userService.GetUserCreditCards(userId);

            return View();
        }
        #endregion

        #region User Wallet
        public IActionResult TransactionList()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userService.GetUserById(userId);
            ViewBag.WalletAmount = _userService.GetAccountBalance(userId).ToString();
            ViewData["UserAvatarViewModel"] = new UserAvatarViewModel()
            {
                ImagePath = user.ImagePath,
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email
            };
            ViewData["TranactionList"] = _userService.GetAllTransactions(userId);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TransactionList(ChargeWallet chargeWallet)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Create a payment flow from the items in the cart.
            // Gets sent to Stripe API.
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                // Stripe calls the URLs below when certain checkout events happen such as success and failure.

                PaymentMethodTypes = new List<string> // Only card available in test mode?
        {
            "card"
        },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long?)chargeWallet.Amount*100, // Price is in USD cents.
                    Currency = "USD",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                      Name = "TwoGirls Wallet",
                    //    Description = "Charge wallet",
                    //    Images = new List<string> { "https://img.icons8.com/color/48/null/wallet--v1.png" }
                    },
                },
                Quantity = 1,
            },
        },
                Mode = "payment", // One-time payment. Stripe supports recurring 'subscription' payments.
                SuccessUrl = "https://Twogirls.somee.com/Success", // Customer paid.
                CancelUrl = "https://Twogirls.somee.com/Failed",  // Checkout cancelled.
                PaymentIntentData = new SessionPaymentIntentDataOptions()
                {
                    Metadata = new Dictionary<string, string>
                    {
                        {"userId",userId.ToString()},
                        {"amount",chargeWallet.Amount.ToString()},
                        {"gatewayType","wallet"},
                    }
                }
            };
            var service = new Stripe.Checkout.SessionService();
            var session = await service.CreateAsync(options);
            
            return Redirect(session.Url);
        }
        #endregion

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel user)
        {
            if (_userService.IsUserExistByEmail(user.Email))
            {
                ModelState.AddModelError("Email", "The email you entered already registered. try Again");
                return View();
            }
            if (ModelState.IsValid)
            {
                //var salt = PasswordHelper.GenerateSalt();

                User newUser = new User()
                {
                    Email = FixedText.FixedEmail(user.Email),
                    Password = HashPassword.Hash(user.Password),
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.lastName,
                    ImagePath = "/image/user-avatar/Default-Avatar.png",
                    IsActive = false,
                    ActiveCode = NameGenerator.GenerateUniqueCode(),
                    RegisterDate = DateTime.Now,
                    RoleId = 4
                };
                _userService.AddUser(newUser);
                #region Send activation Email
                string body = _renderViewWithoutControllerTo.RenderToStringAsync("Email/ConfirmAccount", newUser);
                SendEmail.Send(newUser.Email, "Activation Code | TwoGirls", body);

                #endregion
                ViewBag.isSuccess = true;
                ViewBag.alertText = $"Dear {user.FirstName}, Thank you for registering with us! Your account has been successfully created. Please check your email at " +
                                       $"{FixedText.FixedEmail(user.Email)} for a verification link to activate your account. If you do not receive the email within a few minutes," +
                                       $" please check your spam folder. If you continue to experience issues, please contact our support team for assistance";
                return View("Login");
            }
            else
            {
                return View(user);
            }
        }
        #endregion

        #region Active Account
        public IActionResult ActiveAccount(string id)
        {

            bool userIsActive = _userService.ActiveUserAccount(id);
            if (userIsActive)
            {
                ViewBag.isSuccess = true;
                ViewBag.alertText = "Your account is now active. Please log in to access our services. Thank you!";
                return View("Login");
            }
            else
            {
                ViewBag.isSuccess = false;
                ViewBag.alertText = "We're sorry, but we couldn't find an account with this profile. Please check your profile information and try again.!";
                return View("Login");
            }
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            ViewBag.isLogin = false;
            ViewBag.isSuccess = null;
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel user)
        {
            ViewBag.isLogin = false;
            ViewBag.isSuccess = null;
            if (_userService.IsUserExistByEmail(user.Email))
            {
                var userForCookie = _userService.GetUserByEmailAndPassword(user.Email, user.Password);

                if (userForCookie != null)
                {
                    if (userForCookie.IsActive)
                    {

                        var claims = new List<Claim>()
                        {
                        new Claim(ClaimTypes.NameIdentifier,userForCookie.Id.ToString()),
                        new Claim(ClaimTypes.Email,userForCookie.Email),
                        new Claim(ClaimTypes.Name,userForCookie.FirstName),
                        new Claim(ClaimTypes.Surname,userForCookie.LastName),
                       // new Claim("RoleId", userForCookie.UserRoles.RoleId.ToString())
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var Properties = new AuthenticationProperties() { IsPersistent = user.RememberMe };
                        HttpContext.SignInAsync(principal, Properties);

                        ViewBag.isSuccess = true;
                        ViewBag.alertText = "Login successful. Welcome back!";
                        ViewBag.isLogin = true;
                        return View(user);
                    }
                    else
                    {
                        ViewBag.isSuccess = false;
                        ViewBag.alertText = "it seems like your account is not yet activated. Please check your email for a verification link to activate your account. If you did not receive an email, please check your spam folder. If you continue to experience issues, please contact our support team for assistance. Thank you";
                        ModelState.AddModelError("Email", "This account is not active yet");
                        return View(user);
                    }
                }
                ModelState.AddModelError("Password", "The Password you entered is incorrect. try again");
                return View(user);

            };
            ModelState.AddModelError("Email", "The email you entered is incorrect. try again");
            return View(user);
        }
        #endregion

        #region Logout
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.isLogin = false;
            return Redirect("Login");
        }
        #endregion

        #region Forgot Password
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPassword);
            }
            var user = _userService.GetUserByEmail(forgotPassword.Email);
            if (user == null)
            {
                ViewBag.isSuccess = false;
                ViewBag.alertText = "User not found.Please make sure you have entered the correct email address.";
                ModelState.AddModelError("Email", "User not found");
            }
            string body = _renderViewWithoutControllerTo.RenderToStringAsync("Email/ChangePassword", user);
            SendEmail.Send(user.Email, "Change Password | TwoGirls", body);
            ViewBag.isSuccess = true;
            ViewBag.alertText = "recovery Password email sent successfully.Please check your inbox and follow the instructions to reset your password";
            return View();
        }
        #endregion

        #region Reset Password
        public IActionResult ResetPassword(string id)
        {
            var reset = new ResetPasswordViewModel()
            {
                ActiveCode = id
            };
            return View(reset);
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPassword);
            }
            var status = _userService.ResetPassword(resetPassword.ActiveCode, resetPassword.Password);
            if (status)
            {
                ViewBag.isSuccess = true;
                ViewBag.alertText = "Congratulations! Your password has been successfully changed.";

                return View();
            }
            else
            {
                ViewBag.isSuccess = false;
                ViewBag.alertText = "Your recovery password link is either incorrect or has expired. Please try again or contact support for assistance.";
                return View(resetPassword);
            }
        }
        #endregion


       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

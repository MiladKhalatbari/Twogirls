using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TwoGirls.Core.Convertors;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Generator;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;
namespace twoGirlsOnlineShop.Controllers
{
    public class UserPanelController : Controller
    {
       private readonly IUserService _userService;
       private readonly IOrderService  _orderService;
       private readonly IWebHostEnvironment _hostingEnvironment;
        public UserPanelController(IUserService userService, IOrderService orderService,IWebHostEnvironment hostingEnvironment)
        {
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region User Address
        [HttpPost]
        public IActionResult RemoveAddress(int addressId)
        {
            IEnumerable<Address> addresses;
            string partialView;
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (_userService.RemoveUserAddress(userId, addressId))
            {
                addresses = _userService.GetUserAddresses(userId);
                partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_UserAddresses", addresses);
                return Json(new { success = true, html = partialView });
            }
            addresses = _userService.GetUserAddresses(userId);
            partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_UserAddresses", addresses);
            return Json(new { success = false, html = partialView });
        }
        [HttpPost]
        public IActionResult AddAddress(Address address)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                address.UserId = userId;
                _userService.AddUserAddress(address);
                var addresses = _userService.GetUserAddresses(userId);
                ViewBag.showRadio = true;
                string partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_UserAddresses", addresses);
                return Json(new { success = true, html = partialView });
            }
            else
            {
                string partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_AddUserAddress", address);
                return Json(new { success = false, html = partialView });
            }
        }
        #endregion

        #region User CreditCard
        [HttpPost]
        public IActionResult AddCreditCard(CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                creditCard.UserId = userId;
                _userService.AddUserCreditCard(creditCard);

                var creditCards = _userService.GetUserCreditCards(userId);
                string partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_UserCreditCards", creditCards);
                return Json(new { success = true, html = partialView });
            }
            else
            {
                string partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_AddUserCreditCard", creditCard);
                return Json(new { success = false, html = partialView });
            }
        }
        [HttpPost]
        public IActionResult RemoveCreditCard(int creditCardId)
        {
            IEnumerable<CreditCard> creditCards;
            string partialView;
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (_userService.RemoveUserCreditCard(userId, creditCardId))
            {
                creditCards = _userService.GetUserCreditCards(userId);
                partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_UserCreditCards", creditCards);
                return Json(new { success = true, html = partialView });
            }
            creditCards = _userService.GetUserCreditCards(userId);
            partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_UserCreditCards", creditCards);
            return Json(new { success = false, html = partialView });
        }
        #endregion

        #region User Avatar

        [HttpPost]
        public IActionResult ChangeAvatar(UserAvatarViewModel userAvatar)
        {

            if (ModelState.IsValid && userAvatar.NewImage.IsImage())
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (userAvatar.ImagePath != "/image/user-avatar/Default-Avatar.png")
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    string path = userAvatar.ImagePath.TrimStart('/'); // Remove the leading forward slash
                    string oldfilePath = Path.Combine(webRootPath, path);

                    if (System.IO.File.Exists(oldfilePath))
                    {
                        System.IO.File.Delete(oldfilePath);
                    }
                }
                var fileName = Path.GetFileNameWithoutExtension(userAvatar.NewImage.FileName);
                var extension = Path.GetExtension(userAvatar.NewImage.FileName);
                var uniqueFileName = $"{fileName}_{NameGenerator.GenerateUniqueCode()}{extension}";
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "image", "user-avatar", uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    userAvatar.NewImage.CopyTo(fileStream);
                }
                var imagePath = "/image/user-avatar/" + uniqueFileName;
                userAvatar = _userService.ChangeAvatar(userId, imagePath);
                string partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_UserAvatar", userAvatar);
                return Json(new { success = true, html = partialView });
            }
            else
            {
                string partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_UserAvatar", userAvatar);
                return Json(new { success = false, html = partialView });
            }
        }
        #endregion

        #region User Info
        [HttpPost]
        public async Task<IActionResult> EditUser(EditProfileViewModel editProfile)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                editProfile.Id = userId;
                _userService.UpdateUser(editProfile);

                var identity = User.Identity as ClaimsIdentity;
                identity.RemoveClaim(identity.FindFirst(ClaimTypes.Email));
                identity.RemoveClaim(identity.FindFirst(ClaimTypes.Name));
                identity.RemoveClaim(identity.FindFirst(ClaimTypes.Surname));
                identity.AddClaim(new Claim(ClaimTypes.Email, editProfile.Email));
                identity.AddClaim(new Claim(ClaimTypes.Name, editProfile.FirstName));
                identity.AddClaim(new Claim(ClaimTypes.Surname, editProfile.LastName));

                var authenticateResult = await HttpContext.AuthenticateAsync();
                var authProperties = authenticateResult.Properties;

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);
                string partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_UserEditInfo", editProfile);

                return Json(new { success = true, html = partialView });
            }
            else
            {
                string partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_UserEditInfo", editProfile);
                return Json(new { success = false, html = partialView });
            }
        }

        #endregion

        #region Change Password
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (ModelState.IsValid)
            {
                if (_userService.ChangePassword(changePassword.Id, changePassword.OldPassword, changePassword.Password))
                {
                    return Json(new { success = true });
                }
                string partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_ChangePasswordForm", changePassword);
                return Json(new { success = false, isIncorrect = true, html = partialView });
            }
            else
            {
                string partialView = RenderViewToString.RenderRazorViewToString(this, "UserPanel/_ChangePasswordForm", changePassword);
                return Json(new { success = false, isIncorrect = false, html = partialView });
            }
        }
        #endregion

        #region User Orders
        public IActionResult OrderList()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userService.GetUserById(userId);
            ViewData["UserAvatarViewModel"] = new UserAvatarViewModel()
            {
                ImagePath = user.ImagePath,
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email
            };
            ViewData["OrderList"] = _orderService.GetAllOrdersByUserId(userId);
            return View();
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoGirls.Core.Convertors;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Generator;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Entities;

namespace twoGirlsOnlineShop.Pages.Admin.Users
{
    [PermissionChecker(9)]
    public class CreateOrEditModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateOrEditModel(IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _hostingEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public CreateOrEditUserForAdmin CreateOrEditUserForAdmin { get; set; }
        public void OnGet(int id = 0)
        {
            CreateOrEditUserForAdmin = new CreateOrEditUserForAdmin
            {
                Roles = _userService.GetAllRoles()
            };
            if (id == 0)
            {
                CreateOrEditUserForAdmin.User = new User();
            }
            else
            {
                CreateOrEditUserForAdmin.User = _userService.GetUserById(id);
            }
        }
        public IActionResult OnPost()
        {
            CreateOrEditUserForAdmin.Roles = _userService.GetAllRoles();

            if (ModelState.IsValid)
            {
                var newUser = new User()
                {
                    Email = FixedText.FixedEmail(CreateOrEditUserForAdmin.User.Email),
                    PhoneNumber = CreateOrEditUserForAdmin.User.PhoneNumber,
                    FirstName = CreateOrEditUserForAdmin.User.FirstName,
                    LastName = CreateOrEditUserForAdmin.User.LastName,
                    IsActive = CreateOrEditUserForAdmin.User.IsActive,
                    ActiveCode = NameGenerator.GenerateUniqueCode(),
                    RoleId = CreateOrEditUserForAdmin.User.RoleId,
                    Id = CreateOrEditUserForAdmin.User.Id
                };

                if (CreateOrEditUserForAdmin.formFile != null && CreateOrEditUserForAdmin.formFile.IsImage())
                {
                    var fileName = Path.GetFileNameWithoutExtension(CreateOrEditUserForAdmin.formFile.FileName);
                    var extension = Path.GetExtension(CreateOrEditUserForAdmin.formFile.FileName);
                    var uniqueFileName = $"{fileName}_{NameGenerator.GenerateUniqueCode()}{extension}";
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "image", "user-avatar", uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        CreateOrEditUserForAdmin.formFile.CopyTo(fileStream);
                    }
                    newUser.ImagePath = "/image/user-avatar/" + uniqueFileName;

                    if (CreateOrEditUserForAdmin.User.ImagePath != "/image/user-avatar/Default-Avatar.png")
                    {
                        var webRootPath = _hostingEnvironment.WebRootPath;
                        var imagePath = CreateOrEditUserForAdmin.User.ImagePath.TrimStart('/'); // Remove the leading forward slash
                        var oldfilePath = Path.Combine(webRootPath, imagePath);

                        if (System.IO.File.Exists(oldfilePath))
                        {
                            System.IO.File.Delete(oldfilePath);
                        }
                    }
                }
                else if (CreateOrEditUserForAdmin.User.Id == 0)
                {
                    newUser.ImagePath = "/image/user-avatar/Default-Avatar.png";

                }
                else
                {
                    newUser.ImagePath = CreateOrEditUserForAdmin.User.ImagePath;
                }

                if (CreateOrEditUserForAdmin.User.Id == 0)
                {
                    newUser.Password = HashPassword.Hash(CreateOrEditUserForAdmin.Password);
                    newUser.RegisterDate = DateTime.Now;
                    _userService.AddUser(newUser);
                    return RedirectToPage("Index");
                }
                else
                {
                    if (string.IsNullOrEmpty(CreateOrEditUserForAdmin.Password))
                    {
                        newUser.Password = CreateOrEditUserForAdmin.User.Password;
                    }
                    else
                    {
                        newUser.Password = HashPassword.Hash(CreateOrEditUserForAdmin.Password);
                    }
                    newUser.RegisterDate = CreateOrEditUserForAdmin.User.RegisterDate;
                    _userService.EditUserForAdmin(newUser);
                    return RedirectToPage("Index");
                }

            }

            return Page();
        }
    }
}

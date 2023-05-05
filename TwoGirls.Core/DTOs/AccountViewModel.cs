using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TwoGirls.Core.DTOs
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(64)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "last name")]
        public string lastName { get; set; }
        [Required]
        [MaxLength(64)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(16)]
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [MinLength(8)]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "Password must contain letters and numbers")]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please accept the privacy policy.")]
        [Compare("PrivacyPolicyChecked", ErrorMessage = "Please accept the privacy policy.")]
        public bool PrivacyPolicy { get; set; }

        [Display(Name = "Privacy Policy")]
        public bool PrivacyPolicyChecked { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [MaxLength(64)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [MaxLength(64)]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
       public string ActiveCode { get; set; }

        [Required]
        [MinLength(8)]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "Password must contain letters and numbers")]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(8)]
        [Display(Name = "Old Password")]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "Password must contain letters and numbers")]
        public string OldPassword { get; set; }
        [Required]
        [MinLength(8)]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "Password must contain letters and numbers")]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
    public class EditProfileViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(64)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(16)]
        public string? PhoneNumber { get; set; }
    }
    public class UserAvatarViewModel
    {
        [MaxLength(200)]
        public string? ImagePath { get; set; }
        [Required(ErrorMessage = "Please select a file.")]
        //[FileExtensions(Extensions = "jpg,jpeg,png,gif,JPG,JPEG,PNG,GIF", ErrorMessage = "Please select a file with a valid image extension (jpg, jpeg, png, gif).")]
        public IFormFile NewImage { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set;}
    }
    public class ChargeWallet
    {
        [Required]
        public decimal Amount { get; set; }
    }
}
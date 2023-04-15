using System.ComponentModel.DataAnnotations;

namespace twoGirlsOnlineShop.Models
{
    public class UserViewModel
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

        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(64)]
        [MinLength(8)]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "Password must contain letters and numbers")]
        public string Password { get; set; }
        [Required]
        [MaxLength(64)]
        [MinLength(8)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }
        [Required(ErrorMessage = "Please accept the privacy policy.")]
        [Compare("PrivacyPolicyChecked", ErrorMessage = "Please accept the privacy policy.")]
        public bool PrivacyPolicy { get; set; }

        [Display(Name = "Privacy Policy")]
        public bool PrivacyPolicyChecked { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FPTAppDev.ViewModel
{
    public class CreateStaffViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The Name cannot exceed 255 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Enter a value between 1 and 1000")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
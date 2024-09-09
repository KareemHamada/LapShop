using System.ComponentModel.DataAnnotations;

namespace LapShop.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="Please enter first name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter password")]
        [StringLength(100,MinimumLength =8,ErrorMessage ="please enter 8 character")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string Password { get; set; }


        public string ReturnUrl { get; set; }
    }
}

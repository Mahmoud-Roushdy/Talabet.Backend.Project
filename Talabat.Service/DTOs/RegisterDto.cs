using System.ComponentModel.DataAnnotations;

namespace Talabat.Service.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$",
           ErrorMessage = "Password must be at least 6 characters long, contain one uppercase letter, one number, and one special character.")]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber  { get; set; }
   
    }
}

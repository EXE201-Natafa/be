using System.ComponentModel.DataAnnotations;

namespace Natafa.Api.Models.AuthenticationModel
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Customer Name is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}

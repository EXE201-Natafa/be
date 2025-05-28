using System.ComponentModel.DataAnnotations;

namespace Natafa.Api.Models.UserModel
{
    public class UpdateProfileRequest
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateOnly Birthday { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}

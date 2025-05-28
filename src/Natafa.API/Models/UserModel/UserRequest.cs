namespace Natafa.Api.Models.UserModel
{
    public class UserRequest
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateOnly? Birthday { get; set; }
    }
}

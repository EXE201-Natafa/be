namespace Natafa.Api.Models.UserModel
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public decimal WalletBalance { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

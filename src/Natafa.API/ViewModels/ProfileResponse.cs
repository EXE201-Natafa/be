namespace Natafa.Api.ViewModels
{
    public class ProfileResponse
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public decimal? WalletBalance { get; set; }

        public DateOnly? Birthday { get; set; }

        public bool ConfirmedEmail { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Image { get; set; }
    }
}

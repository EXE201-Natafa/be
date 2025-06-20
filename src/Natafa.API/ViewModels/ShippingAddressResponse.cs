namespace Natafa.Api.ViewModels
{
    public class ShippingAddressResponse
    {
        public int ShippingAddressId { get; set; }

        public string FullName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public bool IsDefault { get; set; }
    }
}

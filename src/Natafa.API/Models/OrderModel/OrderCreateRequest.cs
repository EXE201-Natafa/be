using System.ComponentModel.DataAnnotations;

namespace Natafa.Api.Models.OrderModel
{
    public class OrderCreateRequest
    {
        [Required]
        public int PaymentMethodId { get; set; }
        [Required]
        public bool InRegion { get; set; }
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        public List<OrderDetailCreateRequest> OrderDetailRequests { get; set; } = null!;
    }

    public class OrderDetailCreateRequest
    {
        [Required]
        public int ProductDetailId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}

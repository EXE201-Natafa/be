using Natafa.Api.Models.VoucherModel;
using Natafa.Domain.Entities;

namespace Natafa.Api.ViewModels
{
    public class OrderResponse
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public string OrderCode { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public VoucherResponse? Voucher { get; set; }

        public List<OrderDetailResponse> OrderDetails { get; set; } = null!;

        public List<OrderTrackingResponse> OrderTrackings { get; set; } = null!;

        public TransactionResponse? Transaction { get; set; }
    }

    public class OrderDetailResponse
    {
        public int OrderDetailId { get; set; }

        public int ProductDetailId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }

    public class OrderTrackingResponse
    {
        public string Status { get; set; } = null!;

        public DateTime UpdateTime { get; set; }
    }
}

using Natafa.Api.Models.VoucherModel;
using Natafa.Domain.Entities;

namespace Natafa.Api.ViewModels
{
    public class OrderResponse
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }

        public VoucherResponse Voucher { get; set; }

        public List<OrderDetailResponse> Details { get; set; } = null!;

        public List<OrderStatusResponse> Statuses { get; set; } = null!;

        public TransactionResponse Transaction { get; set; }
    }

    public class OrderDetailResponse
    {
        public int OrderDetailId { get; set; }

        public int? BlindBoxId { get; set; }

        public int? PackageId { get; set; }

        public decimal Price { get; set; }
    }

    public class OrderStatusResponse
    {
        public string? Status { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}

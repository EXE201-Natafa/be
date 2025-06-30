
namespace Natafa.Api.Models.VnPayModel
{
    public class VnPaymentRequestModel
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}


namespace Natafa.Api.Models.VnPayModel
{
    public class VnPaymentRequestModel
    {
        public int RelateId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Type { get; set; }
    }
}

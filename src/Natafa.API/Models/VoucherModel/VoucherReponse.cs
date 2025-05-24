namespace Natafa.Api.Models.VoucherModel
{
    public class VoucherResponse
    {
        public int VoucherId { get; set; }
        public string VoucherName { get; set; } 
        public string VoucherCode { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UsageLimit { get; set; }
        public bool IsActive { get; set; }
    }



}

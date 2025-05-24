namespace Natafa.Api.ViewModels
{
    public class RevenueByMonthResponse
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
    }

    public class RevenueByQuarterResponse
    {
        public int Quarter { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
    }

    public class RevenueByYearResponse
    {
        public int Year { get; set; }
        public decimal Amount { get; set; }
    }
}

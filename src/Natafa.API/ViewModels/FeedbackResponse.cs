namespace Natafa.Api.ViewModels
{
    public class TotalFeedbacksByMonthResponse
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int TotalFeedbacks { get; set; }
    }

    public class TotalFeedbacksByQuarterResponse
    {
        public int Quarter { get; set; }
        public int Year { get; set; }
        public int TotalFeedbacks { get; set; }
    }

    public class TotalFeedbacksByYearResponse
    {
        public int Year { get; set; }
        public int TotalFeedbacks { get; set; }
    }
}

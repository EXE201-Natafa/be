namespace Natafa.Api.Models.FeedbackModel
{
    public class FeedbackResponse
    {
        public int FeedbackId { get; set; }
        public float Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public int ProductId { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public UserFeedback User { get; set; } = null!;
    }
    public class UserFeedback
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Image { get; set; }
    }

}

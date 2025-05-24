using System.ComponentModel.DataAnnotations;

namespace Natafa.Api.Models.FeedbackModel
{
    public class FeedbackRequest
    {
        //[Required]
        //public int ProductId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}


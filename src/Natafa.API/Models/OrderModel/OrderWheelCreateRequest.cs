using System.ComponentModel.DataAnnotations;

namespace Natafa.Api.Models.OrderModel
{
    public class OrderWheelCreateRequest
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public List<int> BlindBoxIds { get; set; } = new List<int>();
    }
}

namespace Natafa.Api.Models
{
    public class PaginateRequest
    {
        public int page { get; set; }
        public int size { get; set; }
        public string? sortBy { get; set; }
        public string? search { get; set; }
        public string? filter { get; set; }
    }


}

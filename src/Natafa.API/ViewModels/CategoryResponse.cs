namespace Natafa.Api.ViewModels
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public List<SubcategoryResponse> Subcategories { get; set; } = new List<SubcategoryResponse>();
    }
    public class SubcategoryResponse
    {
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; } = null!;
    }
}

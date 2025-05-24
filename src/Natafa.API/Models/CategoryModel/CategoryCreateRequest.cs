using System.ComponentModel.DataAnnotations;

namespace Natafa.Api.Models.CategoryModel
{
    public class CategoryCreateRequest
    {
        [Required]
        public string CategoryName { get; set; } = null!;
        public List<SubcategoryCreateRequest> Subcategories { get; set; } = new List<SubcategoryCreateRequest>();
    }

    public class SubcategoryCreateRequest
    {
        [Required]
        public string SubcategoryName { get; set; } = null!;
    }
}
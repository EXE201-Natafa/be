using System.ComponentModel.DataAnnotations;

namespace Natafa.Api.Models.CategoryModel
{
    public class CategoryUpdateRequest
    {   
        [Required]
        public string CategoryName { get; set; } = null!;
        public List<SubcategoryUpdateRequest> Subcategories { get; set; } = new List<SubcategoryUpdateRequest>();
    }

    public class SubcategoryUpdateRequest
    {
        [Required]
        public int SubcategoryId { get; set; }
        [Required]
        public string SubcategoryName { get; set; } = null!;
    }
}

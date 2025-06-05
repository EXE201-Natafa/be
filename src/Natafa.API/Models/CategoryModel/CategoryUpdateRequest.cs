using System.ComponentModel.DataAnnotations;

namespace Natafa.Api.Models.CategoryModel
{
    public class CategoryUpdateRequest
    {   
        [Required]
        public string CategoryName { get; set; } = null!;
        public IFormFile? Image { get; set; }
        public bool IsImageUpdate { get; set; } = false;
        public int? ParentCategoryId { get; set; }
    }
}

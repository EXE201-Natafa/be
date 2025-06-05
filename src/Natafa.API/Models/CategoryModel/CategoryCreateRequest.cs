using System.ComponentModel.DataAnnotations;

namespace Natafa.Api.Models.CategoryModel
{
    public class CategoryCreateRequest
    {
        [Required]
        public string CategoryName { get; set; } = null!;
        public IFormFile? Image { get; set; }
        public int? ParentCategoryId { get; set; } 
    }
}
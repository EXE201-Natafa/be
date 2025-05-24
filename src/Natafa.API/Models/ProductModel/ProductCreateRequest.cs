using System.ComponentModel.DataAnnotations;

namespace Natafa.Api.Models.ProductModel
{
    public class ProductCreateRequest
    {
        [Required]
        public string ProductName { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Material { get; set; } = null!;
        [Required]
        public int SubcategoryId { get; set; }
        public List<ProductDetailCreateRequest> ProductDetails { get; set; } = new List<ProductDetailCreateRequest>();
        public List<IFormFile> ProductImages { get; set; } = new List<IFormFile>();
    }
    public class ProductDetailCreateRequest
    {
        [Required]
        public string Size { get; set; } = null!;

        public decimal? Weight { get; set; }

        public decimal? Height { get; set; }

        public decimal? Width { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Discount { get; set; }
    }    
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Natafa.Api.Models.ProductModel
{
    public class ProductUpdateRequest
    {
        [Required]
        public string ProductName { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Material { get; set; } = null!;
        [Required]
        public int CategoryId { get; set; }
        public bool IsUpdateImage { get; set; }
        public List<ProductDetailUpdateRequest> ProductDetails { get; set; } = new List<ProductDetailUpdateRequest>();        
        public List<IFormFile> ProductImages { get; set; } = new List<IFormFile>();
    }

    public class ProductDetailUpdateRequest
    {
        [Required]
        public int ProductDetailId { get; set; }
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

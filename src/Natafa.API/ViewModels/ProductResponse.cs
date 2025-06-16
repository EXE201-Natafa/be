namespace Natafa.Api.ViewModels
{
    public class ProductResponse
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Summary { get; set; }

        public string Material { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public string? Image { get; set; }

        public bool Status { get; set; }

        public CategoryResponse Category { get; set; } = null!;
    }

    public class ProductDetailResponse
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Summary { get; set; }

        public string Material { get; set; } = null!;

        public int CategoryId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Status { get; set; }

        public CategoryResponse Category { get; set; } = null!;

        public List<DetailProduct> ProductDetails { get; set; } = new List<DetailProduct>();

        public List<string> Images { get; set; } = new List<string>();
    }

    public class DetailProduct
    {
        public int ProductDetailId { get; set; }

        public string Size { get; set; } = null!;

        public decimal? Weight { get; set; }

        public decimal Height { get; set; }

        public decimal Width { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Discount { get; set; }
    }
}

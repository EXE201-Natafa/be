namespace Natafa.Api.ViewModels
{
    public class CartResponse
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
        // Product information
        public string ProductName { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        
        // Product detail information
        public string Size { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int StockQuantity { get; set; }
        
        // Calculated fields
        public decimal TotalPrice => Price * Quantity;
        public decimal DiscountedPrice => Price * (1 - Discount / 100);
        public decimal TotalDiscountedPrice => DiscountedPrice * Quantity;
    }
}


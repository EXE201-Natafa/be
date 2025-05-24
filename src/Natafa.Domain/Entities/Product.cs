using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Summary { get; set; }

    public string Material { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public bool Status { get; set; }

    public int SubcategoryId { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual Subcategory Subcategory { get; set; } = null!;

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}

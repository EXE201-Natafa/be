using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class ProductDetail
{
    public int ProductDetailId { get; set; }

    public string Size { get; set; } = null!;

    public decimal Weight { get; set; }

    public decimal Height { get; set; }

    public decimal Width { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal Discount { get; set; }

    public int ProductId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Product Product { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int ProductDetailId { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ProductDetail ProductDetail { get; set; } = null!;
}

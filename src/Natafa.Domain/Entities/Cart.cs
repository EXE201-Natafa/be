using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class Cart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int ProductDetailId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ProductDetail ProductDetail { get; set; } = null!;
}


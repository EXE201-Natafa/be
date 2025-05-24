using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class Subcategory
{
    public int SubcategoryId { get; set; }

    public string SubcategoryName { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

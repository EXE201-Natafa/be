using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class OrderTracking
{
    public int TrackingId { get; set; }

    public int OrderId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public virtual Order Order { get; set; } = null!;
}

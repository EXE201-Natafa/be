using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class ShippingPriceTable
{
    public int ShippingPriceTableId { get; set; }

    public decimal FromWeight { get; set; }

    public decimal? ToWeight { get; set; }

    public decimal InRegion { get; set; }

    public decimal OutRegion { get; set; }

    public decimal? Pir { get; set; }

    public decimal? Por { get; set; }
}

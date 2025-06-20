using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class ShippingAddress
{
    public int ShippingAddressId { get; set; }

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool IsDefault { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}

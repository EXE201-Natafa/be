using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public string? OrderCode { get; set; }

    public decimal TotalAmount { get; set; }

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public decimal ShippingPrice { get; set; }

    public int UserId { get; set; }

    public int? VoucherId { get; set; }

    public int PaymentMethodId { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;

    public virtual Voucher? Voucher { get; set; }
}

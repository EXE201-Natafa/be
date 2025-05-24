using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;
}

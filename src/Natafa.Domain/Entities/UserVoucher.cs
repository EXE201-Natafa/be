using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class UserVoucher
{
    public int UserVoucherId { get; set; }

    public DateTime? RedeemedDate { get; set; }

    public bool Status { get; set; }

    public int UserId { get; set; }

    public int VoucherId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Voucher Voucher { get; set; } = null!;
}

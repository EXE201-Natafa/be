using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class RefreshToken
{
    public int RefreshTokenId { get; set; }

    public string Token { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}

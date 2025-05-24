using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public float Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool Status { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public virtual ICollection<FeedbackImage> FeedbackImages { get; set; } = new List<FeedbackImage>();

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

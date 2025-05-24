using System;
using System.Collections.Generic;

namespace Natafa.Domain.Entities;

public partial class FeedbackImage
{
    public int FeedbackImageId { get; set; }

    public string Url { get; set; } = null!;

    public int FeedbackId { get; set; }

    public virtual Feedback Feedback { get; set; } = null!;
}

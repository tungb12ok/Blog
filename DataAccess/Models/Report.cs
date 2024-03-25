using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Report
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ArticleId { get; set; }

    public string? ReportContent { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

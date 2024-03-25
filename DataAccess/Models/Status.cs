using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string? Status1 { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}

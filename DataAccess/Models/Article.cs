using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Article
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string ArticleContent { get; set; } = null!;

    public int FieldId { get; set; }

    public DateTime? DatePublished { get; set; }

    public string? Image { get; set; }

    public int? Vote { get; set; }

    public int? StatusId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Field Field { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual Status? Status { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}

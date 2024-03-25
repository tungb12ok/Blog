using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Field
{
    public int Id { get; set; }

    public string FieldName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

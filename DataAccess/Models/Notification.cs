using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string NotificationContent { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}

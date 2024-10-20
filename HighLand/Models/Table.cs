using System;
using System.Collections.Generic;

namespace HighLand.Models;

public partial class Table
{
    public int TableId { get; set; }

    public string TableNumber { get; set; } = null!;

    public bool? Status { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

using System;
using System.Collections.Generic;

namespace HighLand.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? Tax { get; set; }

    public decimal? Discount { get; set; }

    public bool? Status { get; set; }

    public string? PaymentMethod { get; set; }

    public bool OrderType { get; set; }

    public int? TableId { get; set; }

    public int? InvoiceId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Table? Table { get; set; }

    public virtual User? User { get; set; }
}

using System;
using System.Collections.Generic;

namespace HighLand.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int? OrderId { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public string InvoiceCode { get; set; } = null!;

    public bool? PaymentStatus { get; set; }

    public string? PaymentMethod { get; set; }

    public decimal? Amount { get; set; }

    public virtual Order? Order { get; set; }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace HighLand.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public string? Image { get; set; }
    public string ImagePath => Path.Combine("F:/SE/ky9/PRN212/HighLand/HighLand/images/", Image);

    public int? UnitsInStock { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual ProductCategory? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

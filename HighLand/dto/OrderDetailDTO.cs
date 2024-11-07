﻿using System;
using System.Collections.Generic;

namespace HighLand.Models;

public partial class OrderDetailDTO
{
    public int? ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal? TotalPrice { get; set; }
}

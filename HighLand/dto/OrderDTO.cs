using System;
using System.Collections.Generic;

namespace HighLand.Models;

public partial class OrderDTO
{
    public int OrderId { get; set; }
    public string TableName { get; set; }
    public string UserName { get; set; }
    public string CustomerName { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? Tax { get; set; }
    public decimal? TotalPayment { get; set; }
    public List<OrderDetailDTO> OrderDetails { get; set; }

}

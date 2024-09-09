using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Product
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? Quantity { get; set; }
}

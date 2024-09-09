using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Invoice
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int? Bill { get; set; }

    public int? NumberOfSales { get; set; }
}

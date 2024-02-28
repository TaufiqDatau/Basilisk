using System;
using System.Collections.Generic;

namespace Basilisk.DataAccess.Models;

public partial class CompleteProduct
{
    public string ProductName { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string SupplierCompanyName { get; set; } = null!;

    public decimal Price { get; set; }
}

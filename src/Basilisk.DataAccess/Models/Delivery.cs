using System;
using System.Collections.Generic;

namespace Basilisk.DataAccess.Models;

public partial class Delivery
{
    public long Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Phone { get; set; }

    public decimal Cost { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

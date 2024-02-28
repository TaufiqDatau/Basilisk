using System;
using System.Collections.Generic;

namespace Basilisk.DataAccess.Models;

public partial class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? SupplierId { get; set; }

    public long CategoryId { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int OnOrder { get; set; }

    public bool Discontinue { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier? Supplier { get; set; }
}

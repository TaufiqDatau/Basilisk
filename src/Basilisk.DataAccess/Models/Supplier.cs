using System;
using System.Collections.Generic;

namespace Basilisk.DataAccess.Models;

public partial class Supplier
{
    public long Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string ContactPerson { get; set; } = null!;

    public string JobTitle { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? DeleteDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

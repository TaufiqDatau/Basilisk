using System;
using System.Collections.Generic;

namespace Basilisk.DataAccess.Models;

public partial class Order
{
    public string InvoiceNumber { get; set; } = null!;

    public long CustomerId { get; set; }

    public string SalesEmployeeNumber { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public DateTime? DueDate { get; set; }

    public long DeliveryId { get; set; }

    public decimal DeliveryCost { get; set; }

    public string DestinationAddress { get; set; } = null!;

    public string DestinationCity { get; set; } = null!;

    public string DestinationPostalCode { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Delivery Delivery { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Salesman SalesEmployeeNumberNavigation { get; set; } = null!;
}

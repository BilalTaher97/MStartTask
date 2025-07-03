using System;
using System.Collections.Generic;

namespace KafanaTask.Server.Models;

public partial class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public DateTime ServerDateTime { get; set; }

    public DateTime DateTimeUtc { get; set; }

    public decimal TotalAmount { get; set; }

    public string Currency { get; set; } = null!;

    public string StatusEn { get; set; } = null!;

    public string StatusAr { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

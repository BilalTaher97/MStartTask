using System;
using System.Collections.Generic;

namespace KafanaTask.Server.Models;

public partial class Product
{
    public int Id { get; set; }

    public DateTime ServerDateTime { get; set; }

    public DateTime DateTimeUtc { get; set; }

    public DateTime? UpdateDateTimeUtc { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string StatusEn { get; set; } = null!;

    public string StatusAr { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

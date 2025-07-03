using System;
using System.Collections.Generic;

namespace KafanaTask.Server.Models;

public partial class Customer
{
    public int Id { get; set; }

    public DateTime ServerDateTime { get; set; }

    public DateTime DateTimeUtc { get; set; }

    public DateTime? UpdateDateTimeUtc { get; set; }

    public DateTime? LastLoginDateTimeUtc { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string StatusEn { get; set; } = null!;

    public string StatusAr { get; set; } = null!;

    public string? GenderEn { get; set; }

    public string? GenderAr { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? Photo { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

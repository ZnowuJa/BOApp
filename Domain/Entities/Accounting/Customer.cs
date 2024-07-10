using System;
using System.Collections.Generic;

namespace Domain.Entities.Accounting;

public class Customer
{
    public long KontrahentId { get; set; }

    public string? Nazwa { get; set; }

    public string? Nip { get; set; }

    public bool Przelew { get; set; }
}

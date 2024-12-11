using System;
using System.Collections.Generic;

namespace Domain.Entities.BNP;

public partial class Bnp20
{
    public string Txid { get; set; } = null!;

    public string Instrid { get; set; } = null!;

    public DateOnly? Data { get; set; }

    public string? Waluta { get; set; }

    public string? Kwota { get; set; }

    public string? Cdtdbtind { get; set; }

    public string? Nazwa { get; set; }

    public string? Panstwo { get; set; }

    public string? Adres { get; set; }

    public string? Iban { get; set; }

    public string? Tytul { get; set; }
}

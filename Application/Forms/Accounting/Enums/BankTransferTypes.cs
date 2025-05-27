using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forms.Accounting.Enums;
public static class BankTransferTypes
{
    public static readonly List<BankTransferTypeItem> All = new()
    {
        new("ZWROT_KOREKTA", "Zwrot do korekty", 2),
        new("UZYWANE", "Używane", 1),
        new("POLISY", "Polisy", 1),
        new("PODATKOWE", "Podatkowe", 1),
        new("INNE", "Inne", 1),
        new("ZWROT_NADPLATY", "Zwrot nadpłaty", 2),
        new("PROFORMA", "Proforma", 1),
        new("ZWROT_TU", "Zwrot do TU", 2),
        new("ADMINISTRACYJNE", "Administracyjne", 1),
        new("OKULARY", "Okulary", 2),
        new("NOWE_DEALER", "Nowe od Dealera", 1),
        new("CLO", "Cło", 1),
        new("PCC", "PCC", 1),
        new("CEPIK", "CEPIK", 1),
    };

    public record BankTransferTypeItem(string Value, string Display, int AttCount);

    public static IEnumerable<string> DisplayNames => All.Select(f => f.Display);

    public static string? GetDisplayName(string value) =>
        All.FirstOrDefault(f => f.Value == value)?.Display;

    public static string? GetValueByDisplay(string display) =>
        All.FirstOrDefault(f => f.Display == display)?.Value;
    public static int? GetAttCountByDisplay(string display) =>
        All.FirstOrDefault(f => f.Display == display)?.AttCount;

}

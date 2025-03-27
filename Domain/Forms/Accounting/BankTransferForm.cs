using Domain.Common;

namespace Domain.Forms.Accounting;

public class BankTransferForm : FormTemplate
{
    public BankTransferForm() : base("Polecenie przelewu", "Formularz do poleceń przelewów", "BankTransfers", "PZ", "Accounting", "Rejestracja", 6)
    {
        Statuses = GetDefaultStatuses();
    }
    public static List<string> GetDefaultStatuses()
    {
        return new List<string>
        {
            "Rejestracja", "AprobataL1", "AprobataL2", "AprobataL3", "AprobataL4","Ksiegowosc", "KsiegowoscTL",  "Kasa", "WyslaneDoRobota", "BladRobota", "Rozliczone", "Zamkniete", "Odrzucone"
        };
    }
}
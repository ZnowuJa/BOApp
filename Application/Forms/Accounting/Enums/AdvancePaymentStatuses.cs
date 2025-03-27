using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forms.Accounting.Enums
{
    public enum AdvancePaymentStatuses
    {
        Rejestracja, //user or just employee
        AprobataL1, // manager of user
        AprobataL2, //dopytać księgowe
        ZaliczkaKasa,
        ZaliczkaKsiegowosc, // Accountants
        WyslaneDoRobota, // BOT
        BladRobota, // Księgowa
        ZaliczkaKsiegowoscTL, // Accountants TLs
        Odrzucone,
        Wyplacone,
        Rozliczone
    }
}

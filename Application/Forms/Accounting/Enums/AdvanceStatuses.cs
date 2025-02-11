using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forms.Accounting.Enums
{
    public enum AdvanceStatuses
    {
        Rejestracja, //user or just employee
        AprobataL1, // manager of user
        AprobataL2, // manager of manager of user
        Ksiegowosc, // Accountants
        WyslaneDoRobota, // BOT
        KsiegowoscTL, // Accountants TLs
        Kasa, // Cashier
        Zakonczone, //
        Odrzucone, // 
    }
}

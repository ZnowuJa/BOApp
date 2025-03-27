using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forms.Accounting.Enums
{
    public enum BusinessTravelStatuses
    { 
        Rejestracja, //user or just employee
        AprobataL1, // manager of user
        AprobataL2, // manager of manager of user
        ZaliczkaKasa, // Cashier
        ZaliczkaKsiegowosc, // Accountants
        ZaliczkaKsiegowoscTL, // Accountants TLs
        WyslaneDoRobota, // BOT
        BladRobota, // Księgowa
        Rozliczenie, // user or just employee
        Ksiegowosc, // Accountants
        KsiegowoscTL, // Accountants TLs
        AprobataL11, // manager of user
        AprobataL12, // some kind of Director
        KasaRozliczenie, // Cashier
        WyslaneDoRobotaRozliczenie, // BOT
        BladRobotaRozliczenie, // Księgowa
        Rozliczone, // Read Every One
        Zamkniete // Probably not necessary
    }
}


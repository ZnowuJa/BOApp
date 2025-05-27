using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forms.Accounting.BuisnessTravelSmallClasses
{
    public class Accommodation()
    {
        public int StageId { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public decimal? Duration { get; set; } = 0;
        public decimal MaxHotelCost { get; set; } = 500;
        public decimal AllowanceRate { get; set; } = 0;
        public string AllowanceRateCurrency { get; set; }
        public bool HasInvoices { get; set; } = false;
        public decimal? InvoicesAmount { get; set; } = 0;
        public int? Nights { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public bool Included { get; set; } = true;

    }
}



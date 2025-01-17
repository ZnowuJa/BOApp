using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forms.Accounting.BuisnessTravelSmallClasses

{
    public class LocalTravel()
    {
        //required local travels in stay city (trams, buses, taxis)
        public int StageId { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public decimal AllowanceRate { get; set; } = 0;
        public string AllowanceRateCurrency { get; set; }
        public int Days { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public decimal Total { get; set; } = 0;
        public bool Included { get; set; } = true;

    }
}


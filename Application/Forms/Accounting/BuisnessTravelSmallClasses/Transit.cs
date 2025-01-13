using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forms.Accounting.BuisnessTravelSmallClasses

{
    public class Transit()
    {
        //transit from public transport station to Accomodation place (
        public int StageId { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public decimal AllowanceRate { get; set; } = 0;
        public string AllowanceRateCurrency { get; set; }
        public int Directions { get; set; } = 0; // 0-none, 1-one way, 2-both ways
        public decimal Total { get; set; } = 0;
        public bool Included { get; set; } = false;
    }
}


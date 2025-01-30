using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels.Accounting;

namespace Application.Forms.Accounting.BuisnessTravelSmallClasses
{
    public class Stage()
    {
        public int Id { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public decimal CountryAllowance { get; set; } = 0;
        public string CountryCurrency { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; } = DateTime.Now;
        public decimal? Duration { get; set; } = 0;
        public TimeSpan TimeSpan { get; set; } = TimeSpan.Zero;
        public bool TimeSpanOK { get; set; } = true;
        public decimal? AllowanceOrigin { get; set; } = 0;
        public decimal? AllowanceOriginValue { get; set; } = 0;
        public decimal? AllowanceAbroad { get; set; } = 0;
        public decimal? AllowanceAbroadValue { get; set; } = 0;
        public NbpCurrencyRateVm NbpCurrencyRateVm { get; set; }
        public bool Included { get; set; } = true;

    }
}



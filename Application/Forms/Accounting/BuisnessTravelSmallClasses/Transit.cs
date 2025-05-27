using Application.ViewModels.Accounting;

namespace Application.Forms.Accounting.BuisnessTravelSmallClasses

{
    public class Transit
    {
        //transit from public transport station to Accomodation place (
        public int StageId { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public decimal AllowanceRate { get; set; } = 0;
        public string AllowanceRateCurrency { get; set; } = string.Empty;
        public int Directions { get; set; } = 0; // 0-none, 1-one way, 2-both ways
        public decimal Total { get; set; } = 0;
        public bool Included { get; set; } = false;

        public Transit()
        {
        }
        public Transit(CountryVm country)
        {
            this.StageId = 1;
            this.CountryCode = country.CountryCode;
            this.CountryName = country.Name;
            this.AllowanceRate = country.Allowance;
            this.AllowanceRateCurrency = country.currencyVm.Name;

        }
    }
}


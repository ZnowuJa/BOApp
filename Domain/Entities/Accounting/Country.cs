using BackOfficeApp_Domain.Common;

namespace Domain.Entities.Accounting
{
    public class Country : SmallAuditableEntity
    {
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public bool IsEU { get; set; }
        public bool IsPL { get; set; }
        public int CurrencyId {  get; set; }
        public decimal Allowance { get; set; }
        public decimal AllowanceFirstDay8H { get; set; }
        public decimal AllowanceFirstDay12H { get; set; }
        public decimal AllowanceNextDay8H { get; set; }
        public decimal AllowanceNextDay12H { get; set; }
        public decimal BreakfastReduction { get; set; }
        public decimal LunchReduction { get; set; }
        public decimal DinnerReduction { get; set; }
        public decimal AccomodationAllowance { get; set; }
        public decimal TravelAllowance { get; set; }
        public decimal LocalTravelAllowance { get; set; }
    }
}

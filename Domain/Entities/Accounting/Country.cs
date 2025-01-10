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
        public decimal TravelAllowance { get; set; }
        public decimal LocalTravelAllowance { get; set; }
        public decimal MaxHotelCost { get; set; }
    }
}

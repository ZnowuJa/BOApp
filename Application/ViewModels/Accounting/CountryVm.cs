using Application.Mappings;

using AutoMapper;

using Domain.Entities.Accounting;

using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Accounting
{
    public class CountryVm : IMapFrom<Country>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CountryCode is required.")]
        [MinLength(2, ErrorMessage = "CountryCode must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "CountryCode cannot exceed 50 characters.")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "Code cannot exceed 50 characters.")]
        public string Name { get; set; }

        public bool IsEU { get; set; }
        public bool IsPL { get; set; }
        public int CurrencyId { get; set; }
        public int? CurrencyVmId { get; set; }
        public string? CurrencyVmName { get; set; }
        public CurrencyVm? currencyVm { get; set; }
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
        public decimal MaxHotelCost { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryVm>().ReverseMap();
        }
    }
}

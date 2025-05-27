using Application.Mappings;

using AutoMapper;

using Domain.Entities.Accounting;

using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Accounting
{
    public class CountryVm : IMapFrom<Country>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kod państwa jest wymagany.")]
        [MinLength(2, ErrorMessage = "Kod państwa musi mieć co najmniej 2 znaki.")]
        [MaxLength(50, ErrorMessage = "Kod państwa nie może przekroczyć 50 znaków.")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Nazwa państwa jest wymagana.")]
        [MinLength(3, ErrorMessage = "Nazwa państwa musi mieć co najmniej 3 znaki.")]
        [MaxLength(50, ErrorMessage = "Nazwa państwa nie może przekroczyć 50 znaków.")]
        public string Name { get; set; }

        public bool IsEU { get; set; }
        public bool IsPL { get; set; }
        public int CurrencyId { get; set; }
        public int? CurrencyVmId { get; set; }
        public string? CurrencyVmName { get; set; }
        public CurrencyVm? currencyVm { get; set; }
        public decimal Allowance { get; set; }
        public decimal TravelAllowance { get; set; }
        public decimal LocalTravelAllowance => Allowance * 0.1m;
        public decimal MaxHotelCost { get; set; } = 200m;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryVm>().ReverseMap();
        }
    }
}

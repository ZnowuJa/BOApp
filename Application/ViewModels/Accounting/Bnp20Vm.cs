using Application.Mappings;
using Domain.Entities.BNP;
using AutoMapper;
using System.Globalization;

namespace Application.ViewModels.Accounting
{
    public class Bnp20Vm : IMapFrom<Bnp20>
    {
        public string Txid { get; set; } = null!;

        public string Instrid { get; set; } = null!;

        public DateOnly Data { get; set; }

        public string? Waluta { get; set; }

        public string? Kwota { get; set; }

        public Decimal Amount => decimal.TryParse(
        Kwota,
        NumberStyles.Number,
        CultureInfo.InvariantCulture,
        out var result) ? result : 0;

        public string? Cdtdbtind { get; set; }

        public string? Nazwa { get; set; }

        public string? Panstwo { get; set; }

        public string? Adres { get; set; }

        public string? Iban { get; set; }

        public string? Tytul { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Bnp20, Bnp20Vm>().ReverseMap();
        }
    }
}

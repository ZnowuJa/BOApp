using Application.Mappings;
using AutoMapper;
using Domain.Entities.BNP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting
{
    public class Bnp55Vm : IMapFrom<Bnp55>
    {
        public string Txid { get; set; } = null!;

        public string Instrid { get; set; } = null!;

        public DateOnly Data { get; set; }

        public string? Waluta { get; set; }

        public string? Kwota { get; set; }

        public Decimal? Amount => decimal.TryParse(
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
            profile.CreateMap<Bnp55, Bnp55Vm>().ReverseMap();
        }
    }
}

using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Application.Mappings;
using Domain.Entities.ITTools.LicenceAutoStacja;

namespace Application.ViewModels.AutoStacja;

public class MysystemPunktComputerVm : IMapFrom<MysystemPunkt>
{
    [Required(ErrorMessage = "Pole Nazwa Komputera jest wymagane.")]
    public string? PunktTelefon { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MysystemPunkt, MysystemPunktVm>().ReverseMap();
    }
}

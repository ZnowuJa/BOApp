using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Application.Mappings;
using Domain.Entities.ITTools.LicenceAutoStacja;

namespace Application.ViewModels.AutoStacja;

public class MysystemPunktVm : IMapFrom<MysystemPunkt>
{
    public long MysystemPunktId { get; set; }

    public long? JednostkaOrgId { get; set; }
    public string? Nazwa { get; set; }
    
    [Required(ErrorMessage = "Pole Nazwa Komputera jest wymagana.")]
    public string? PunktTelefon { get; set; }
    public string? PunktEmail { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<MysystemPunkt, MysystemPunktVm>().ReverseMap();
    }
}

using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Application.Mappings;
using Domain.Entities.ITTools.LicenceAutoStacja;

namespace Application.ViewModels.AutoStacja;

public class MysystemPunktLicenceVm : IMapFrom<MysystemPunkt>
{
/*    public long MysystemPunktId { get; set; }

    public long? JednostkaOrgId { get; set; }*/

    [Required(ErrorMessage = "Pole Nazwa Licencji jest wymagane.")]
    public string? Nazwa { get; set; }

    /// <summary>
    /// Numer kontaktowy do wydruków w zależności  z którego pkt zostanie wykonany wydruk
    /// </summary>
    
/*    [Required(ErrorMessage = "Pole Nazwa Komputera jest wymagana.")]
    public string? PunktTelefon { get; set; }*/

    /// <summary>
    /// Email kontaktowy do wydruków w zależności  z którego pkt zostanie wykonany wydruk
    /// </summary>
    public string? PunktEmail { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<MysystemPunkt, MysystemPunktVm>().ReverseMap();
    }
}

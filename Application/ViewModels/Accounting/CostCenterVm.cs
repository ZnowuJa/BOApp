using Application.Mappings;
using AutoMapper;
using Domain.Entities.Accounting;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Accounting
{
    public class CostCenterVm : IMapFrom<CostCenter>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "MPK jest wymagane.")]
        [MinLength(3, ErrorMessage = "MPK musi mieć co najmniej 3 znaki.")]
        [MaxLength(50, ErrorMessage = "MPK nie może przekraczać 50 znaków.")]
        public string MPK { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        [MinLength(3, ErrorMessage = "Opis musi mieć co najmniej 3 znaki.")]
        [MaxLength(250, ErrorMessage = "Opis nie może przekraczać 250 znaków.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Tekst jest wymagany.")]
        [MinLength(3, ErrorMessage = "Tekst musi mieć co najmniej 3 znaki.")]
        [MaxLength(250, ErrorMessage = "Tekst nie może przekraczać 250 znaków.")]
        public string Text { get; set; }

        public int? StatusId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CostCenter, CostCenterVm>().ReverseMap();
        }
    }
}

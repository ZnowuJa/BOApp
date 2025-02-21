using Application.Mappings;
using AutoMapper;
using Domain.Entities.Accounting;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Accounting
{
    public class VATRateVm : IMapFrom<VATRate>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [MinLength(3, ErrorMessage = "Tytuł musi mieć co najmniej 3 znaki.")]
        [MaxLength(50, ErrorMessage = "Tytuł nie może przekraczać 50 znaków.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Procent jest wymagany.")]
        [Range(0, 100, ErrorMessage = "Procent musi mieścić się w zakresie od 0 do 100.")]
        public double Percentage { get; set; }

        [Required(ErrorMessage = "Informacja jest wymagana.")]
        [MinLength(3, ErrorMessage = "Informacja musi mieć co najmniej 3 znaki.")]
        [MaxLength(250, ErrorMessage = "Informacja nie może przekraczać 250 znaków.")]
        public string Information { get; set; }

        [Required(ErrorMessage = "Kolejność jest wymagana.")]
        [Range(1, int.MaxValue, ErrorMessage = "Kolejność musi być dodatnią liczbą całkowitą.")]
        public int Order { get; set; }

        public int? StatusId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VATRate, VATRateVm>().ReverseMap();
        }

    }
}

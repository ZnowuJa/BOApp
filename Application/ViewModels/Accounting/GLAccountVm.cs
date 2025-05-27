using Application.Mappings;
using AutoMapper;
using Domain.Entities.Accounting;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Accounting
{
    public class GLAccountVm : IMapFrom<GLAccount>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numer konta jest wymagany.")]
        [MinLength(3, ErrorMessage = "Numer konta musi mieć co najmniej 3 znaki.")]
        [MaxLength(50, ErrorMessage = "Numer konta nie może przekraczać 50 znaków.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        [MinLength(3, ErrorMessage = "Opis musi mieć co najmniej 3 znaki.")]
        [MaxLength(250, ErrorMessage = "Opis nie może przekraczać 250 znaków.")]
        public string Description { get; set; }

        public int? StatusId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GLAccount, GLAccountVm>().ReverseMap();
        }
    }
}

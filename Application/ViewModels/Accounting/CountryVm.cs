using Application.Mappings;
using AutoMapper;
using Domain.Entities.Accounting;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Accounting
{
    public class CountryVm : IMapFrom<Country>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        [MinLength(2, ErrorMessage = "Code must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "Code cannot exceed 50 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "Code cannot exceed 50 characters.")]
        public string Name { get; set; }

        public bool IsEU { get; set; }
        public int? StatusId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryVm>().ReverseMap();
        }
    }
}

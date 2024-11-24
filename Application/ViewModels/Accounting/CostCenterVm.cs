using Application.Mappings;
using AutoMapper;
using Domain.Entities.Accounting;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Accounting
{
    public class CostCenterVm : IMapFrom<CostCenter>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "MPK is required.")]
        [MinLength(3, ErrorMessage = "MPK must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "MPK cannot exceed 50 characters.")]
        public string MPK { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MinLength(3, ErrorMessage = "Description must be at least 3 characters long.")]
        [MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        [MinLength(3, ErrorMessage = "Text must be at least 3 characters long.")]
        [MaxLength(250, ErrorMessage = "Text cannot exceed 250 characters.")]
        public string Text { get; set; }

        public int? StatusId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CostCenter, CostCenterVm>().ReverseMap();
        }
    }
}

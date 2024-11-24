using Application.Mappings;
using AutoMapper;
using Domain.Entities.Accounting;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Accounting
{
    public class VATRateVm : IMapFrom<VATRate>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Percentage is required.")]
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100.")]
        public double Percentage { get; set; }

        [Required(ErrorMessage = "Information is required.")]
        [MinLength(3, ErrorMessage = "Information must be at least 3 characters long.")]
        [MaxLength(250, ErrorMessage = "Information cannot exceed 250 characters.")]
        public string Information { get; set; }

        [Required(ErrorMessage = "Order is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Order must be a positive integer.")]
        public int Order { get; set; }
        public int? StatusId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VATRate, VATRateVm>().ReverseMap();
        }
    }
}

using Application.Mappings;
using AutoMapper;
using Domain.Entities.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting
{
    public class GLAccountVm : IMapFrom<GLAccount>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Account Number is required.")]
        [MinLength(3, ErrorMessage = "Account Number must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "Account Number cannot exceed 50 characters.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MinLength(3, ErrorMessage = "Description must be at least 3 characters long.")]
        [MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string Description { get; set; }

        public int? StatusId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GLAccount, GLAccountVm>().ReverseMap();
        }
    }
}

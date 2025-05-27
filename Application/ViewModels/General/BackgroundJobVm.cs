using Application.Mappings;
using AutoMapper;
using Domain.Entities.Administration;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.General
{
    public class BackgroundJobVm : IMapFrom<BackgroundJob>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole AssemblyName jest wymagane.")]
        public string AssemblyName { get; set; } = "Application";

        [Required(ErrorMessage = "Pole JobClass jest wymagane.")]
        public string JobClass { get; set; }

        [Required(ErrorMessage = "Pole JobMethod jest wymagane.")]
        public string JobMethod { get; set; } = "Execute";

        [Required(ErrorMessage = "Pole CronExpression jest wymagane.")]
        public string CronExpression { get; set; }

        public bool Enabled { get; set; }

        public string IsEnabled => Enabled ? "Tak" : "Nie";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BackgroundJob, BackgroundJobVm>().ReverseMap();
        }
    }
}
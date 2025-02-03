using Application.Mappings;
using AutoMapper;
using Domain.Entities.Administration;

namespace Application.ViewModels.General
{
    public class BackgroundJobVm : IMapFrom<BackgroundJob>
    {
        public int Id { get; set; }
        public string AssemblyName { get; set; }
        public string JobClass { get; set; }
        public string JobMethod { get; set; }
        public string CronExpression { get; set; }
        public bool Enabled { get; set; }
        public string IsEnabled => Enabled ? "Tak" : "Nie";
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BackgroundJob, BackgroundJobVm>().ReverseMap();
        }
    }
}

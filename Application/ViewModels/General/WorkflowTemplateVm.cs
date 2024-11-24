using Application.Mappings;

using AutoMapper;

using Domain.WorkFlows;

namespace Application.ViewModels.General;
public class WorkflowTemplateVm :IMapFrom<WorkflowTemplate>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FormClassName { get; set; } = string.Empty;
    public IList<WorkflowStepVm> Steps { get; set; }
    public WorkflowTemplateVm()
    {
        Id = 0;
        Name = string.Empty;
        Steps = new List<WorkflowStepVm>();
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WorkflowTemplate, WorkflowTemplateVm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps))
            .ReverseMap();
    }
}

using Application.Mappings;

using AutoMapper;
using Domain.WorkFlows;

namespace Application.ViewModels.General;
public class WorkflowStepVm :IMapFrom<WorkflowStep>
{
    public int Id { get; set; }
    public int StepNumber { get; set; }
    public string WorkflowRole { get; set; } // values only from Organisation Dictionary
    public string Status { get; set; } // values only from Form BusinessTravelStatuses
    public int WorkflowTemplateId { get; set; }
    public WorkflowTemplateVm WorkflowTemplate { get; set; }

    public WorkflowStepVm()
    {
        Id = 0;
        StepNumber = 0;
        WorkflowRole = string.Empty;
        Status = string.Empty;
        WorkflowTemplateId = 0;
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<WorkflowStep, WorkflowStepVm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src=> src.Id))
            .ForMember(dest => dest.StepNumber, opt => opt.MapFrom(src => src.StepNumber))
            .ForMember(dest => dest.WorkflowRole, opt => opt.MapFrom(src => src.WorkflowRole))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.WorkflowTemplate, opt => opt.MapFrom(src => src.WorkflowTemplate))
            .ReverseMap();
        ;
    }
}

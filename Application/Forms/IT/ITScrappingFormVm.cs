using Domain.WorkFlows;
using Application.ViewModels.General;
using Application.DTOs;
using Domain.Entities.ITWarehouse;
using AutoMapper;
using Domain.Forms.ITForms;
using System.Text.Json;
using Application.Mappings;

namespace Application.Forms.IT;
public class ITScrappingFormVm : IMapFrom<ITScrappingForm>
{

    public int Id { get; set; }
    public string Name { get; set; } = "Złomowanie sprzętu IT";
    public string Description { get; set; } = "Formularz do złomowania sprzętu IT.";
    public string FolderName { get; set; } = "ITScrappingForm";
    public string NumberPrefix { get; set; } = "ITSCRAP";
    public string Status { get; set; }
    public List<string> Statuses { get; set; }
    public int WorkflowTemplateId { get; set; }
    public WorkflowTemplate WorkflowTemplate { get; set; }
    public string? Number { get; set; }
    // Properties specific to Form

    public string? Note { get; set; }
    public int? OperatorId { get; set; }
    public string? OperatorName { get; set; }

    public List<ApprovalVm>? Approvals { get; set; }
    public List<OrganisationRoleForFormVm> Level1Approvers { get; set; }
    public List<OrganisationRoleForFormVm> Level2Approvers { get; set; }
    public string LVL1_EnovaEmpId { get; set; }
    public string LVL2_EnovaEmpId { get; set; }
    public string LVL1_EmployeeName { get; set; }
    public string LVL2_EmployeeName { get; set; }

    public List<FormFileVm> FormFiles { get; set; }
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public ICollection<AssetDTO>? assets { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<ITScrappingForm, ITScrappingFormVm>()
            .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src => DeserializeRoles(src.Level1Approvers)))
            .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => DeserializeRoles(src.Level2Approvers)))
            .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => DeserializeApprovals(src.Approvals)))
            .ReverseMap()
            .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src => SerializeRoles(src.Level1Approvers)))
            .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => SerializeRoles(src.Level2Approvers)))
            .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => SerializeApprovals(src.Approvals)));
    }
    private string SerializeApprovals(List<ApprovalVm> approvals)
    {
        return approvals == null || approvals.Count == 0 ? string.Empty : JsonSerializer.Serialize(approvals);
    }
    private string SerializeRoles(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? string.Empty : JsonSerializer.Serialize(roles);
    }
    private List<ApprovalVm> DeserializeApprovals(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<ApprovalVm>() : JsonSerializer.Deserialize<List<ApprovalVm>>(json);
    }
    private List<OrganisationRoleForFormVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleForFormVm>() : JsonSerializer.Deserialize<List<OrganisationRoleForFormVm>>(json);
    }

}

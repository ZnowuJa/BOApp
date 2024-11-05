using System.Text.Json;

using Application.DTOs;
using Application.Mappings;
using Application.ViewModels;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms.ITForms;
using Domain.WorkFlows;

namespace Application.Forms;
public class ITSaleFormVm : IMapFrom<ITSaleForm>
{
    public int Id { get; set; }
    public string Name { get; set; } = "Sprzedaż sprzętu IT";
    public string Description { get; set; } = "Formularz do sprzedaży sprzętu IT";
    public string FolderName { get; set; } = "ITSaleForm";
    public string NumberPrefix { get; set; } = "ITSALE";
    public string Status { get; set; } = "Rejestracja";
    public List<string> Statuses { get; set; }
    public int WorkflowTemplateId { get; set; } = 3;
    public WorkflowTemplate WorkflowTemplate { get; set; }
    public string? Number { get; set; } = "New sale - not saved";
    // Properties specific to Form


    public string? Note { get; set; }
    public int? OperatorId { get; set; }
    public string? OperatorName { get; set; }


    public List<Approval>? Approvals { get; set; } = new List<Approval>();
    public List<OrganisationRoleForFormVm> Level1Approvers { get; set; } = new List<OrganisationRoleForFormVm>();
    public List<OrganisationRoleForFormVm> Level2Approvers { get; set; } = new List<OrganisationRoleForFormVm>();
    public string LVL1_EnovaEmpId { get; set; } = string.Empty;
    public string LVL2_EnovaEmpId { get; set; } = string.Empty;
    public string LVL1_EmployeeName { get; set; } = string.Empty;
    public string LVL2_EmployeeName { get; set; } = string.Empty;


    public List<FormFileVm> FormFiles { get; set; }
    public int? CompanyId { get; set; }
    public CompanyVm? Company { get; set; }
    public int? EmployeeId {  get; set; }
    public EmployeeVm? Employee { get; set; }
    public ICollection<AssetDTO>? Assets { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<ITSaleForm, ITSaleFormVm>()
            .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src => DeserializeRoles(src.Level1Approvers)))
            .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => DeserializeRoles(src.Level2Approvers)))
            .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => DeserializeApprovals(src.Approvals)));
        
        profile.CreateMap<ITSaleFormVm, ITSaleForm>()
            .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src => SerializeRoles(src.Level1Approvers)))
            .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => SerializeRoles(src.Level2Approvers)))
            .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => SerializeApprovals(src.Approvals)));
    }

    private string SerializeApprovals(List<ViewModels.General.Approval> approvals)
    {
        return approvals == null || approvals.Count == 0 ? string.Empty : JsonSerializer.Serialize(approvals);
    }
    private string SerializeRoles(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? string.Empty : JsonSerializer.Serialize(roles);
    }
    private List<Approval> DeserializeApprovals(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<Approval>() : JsonSerializer.Deserialize<List<Approval>>(json);
    }
    private List<OrganisationRoleForFormVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleForFormVm>() : JsonSerializer.Deserialize<List<OrganisationRoleForFormVm>>(json);
    }

}

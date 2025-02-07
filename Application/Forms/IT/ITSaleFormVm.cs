using System.Text.Json;

using Application.AdHocJobs;
using Application.DTOs;
using Application.ExportModels;
using Application.Interfaces;
using Application.Mappings;
using Application.ViewModels;
using Application.ViewModels.General;
using AutoMapper;

using Domain.Forms.ITForms;
using Domain.WorkFlows;

namespace Application.Forms.IT;
public class ITSaleFormVm : IMapFrom<ITSaleForm>, IFormVm
{
    public ITSaleFormVm()
    {
        Statuses = new List<string>
        {
            "Rejestracja", "W trakcie", "Zamkniete"
        };
    }
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
    public List<ApprovalVm>? Approvals { get; set; } = new List<ApprovalVm>();
    public List<OrganisationRoleForFormVm> Level1Approvers { get; set; } = new List<OrganisationRoleForFormVm>();
    public List<OrganisationRoleForFormVm> Level2Approvers { get; set; } = new List<OrganisationRoleForFormVm>();
    public string LVL1_EnovaEmpId { get; set; } = string.Empty;
    public string LVL2_EnovaEmpId { get; set; } = string.Empty;
    public string LVL1_EmployeeName { get; set; } = string.Empty;
    public string LVL2_EmployeeName { get; set; } = string.Empty;
    public List<FormFileVm> FormFiles { get; set; }
    public int? CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public CompanyVm? Company { get; set; }
    public int? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public EmployeeVm? Employee { get; set; }
    public ICollection<AssetDTO>? Assets { get; set; }
    public List<int>? AssetIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ITSaleForm, ITSaleFormVm>()
            .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src => AppUtils.DeserializeRoles(src.Level1Approvers)))
            .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => AppUtils.DeserializeRoles(src.Level2Approvers)))
            .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => AppUtils.DeserializeApprovals(src.Approvals)))
            .ForMember(dest => dest.AssetIds, opt => opt.MapFrom(src => DeserializeAssetIds2Int(src.AssetIds)))
            .ForMember(dest => dest.FormFiles, opt => opt.MapFrom(src => AppUtils.DeserializeFiles(src.FormFiles)));
            /*.ForMember(dest => dest.Assets, opt => opt.MapFrom(src => src.AssetIds.Select(id => new AssetDTO { Id = id }).ToList()))*/;

        profile.CreateMap<ITSaleFormVm, ITSaleForm>()
            .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src =>
            AppUtils.SerializeRoles(src.Level1Approvers)))
            .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => AppUtils.SerializeRoles(src.Level2Approvers)))
            .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => AppUtils.SerializeApprovals(src.Approvals)))
            .ForMember(dest => dest.AssetIds, opt => opt.MapFrom(src => SerializeAssetIds(src.AssetIds)))
            .ForMember(dest => dest.FormFiles, opt => opt.MapFrom(src => AppUtils.SerializeFiles(src.FormFiles)));

        profile.CreateMap<ITSaleFormVm, ITSaleFormExportModel>()
                .ForMember(dest => dest.AssetIds, opt => opt.MapFrom(src => SerializeAssetIds(src.AssetIds)));
    }
    private string SerializeApprovals(List<ApprovalVm> approvals)
    {
        return approvals == null || approvals.Count == 0 ? string.Empty : JsonSerializer.Serialize(approvals);
    }
    private List<ApprovalVm> DeserializeApprovals(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<ApprovalVm>() : JsonSerializer.Deserialize<List<ApprovalVm>>(json);
    }
    private string SerializeRoles(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? string.Empty : JsonSerializer.Serialize(roles);
    }
    private List<OrganisationRoleForFormVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleForFormVm>() : JsonSerializer.Deserialize<List<OrganisationRoleForFormVm>>(json);
    }
    private string SerializeAssetIds(List<int>? assetIds)
    {
        return assetIds == null || !assetIds.Any()
            ? string.Empty
            : JsonSerializer.Serialize(assetIds);
    }
    private List<int> DeserializeAssetIds2Int(string assetIdsJson)
    {
        return string.IsNullOrEmpty(assetIdsJson)
            ? new List<int>()
            : JsonSerializer.Deserialize<List<int>>(assetIdsJson);
    }
    //private string SerializeFiles(List<FormFileVm> files)
    //{
    //    return files == null || files.Count == 0 ? string.Empty : JsonSerializer.Serialize(files);
    //}

    //private List<FormFileVm> DeserializeFiles(string json)
    //{
    //    return string.IsNullOrEmpty(json) ? new List<FormFileVm>() : JsonSerializer.Deserialize<List<FormFileVm>>(json);
    //}
    

}

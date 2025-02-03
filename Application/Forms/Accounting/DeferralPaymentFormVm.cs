using Application.ExportModels;
using Application.Interfaces;
using Application.Mappings;
using Application.ViewModels.General;

using AutoMapper;
using Domain.Forms;

namespace Application.Forms.Accounting;

public class DeferralPaymentFormVm : IMapFrom<DeferralPaymentForm>, IFormVm
{
    // Properties from FormTemplate
    public int Id { get; set; }
    public string Name { get; set; } = "Płatność odroczona";
    public string Description { get; set; } = "Formularz do ustawiania płatności odroczonych w Autostacji.";
    public string FolderName { get; set; } = "deferralPayment";
    public string NumberPrefix { get; set; } = "DP";
    public string Status { get; set; }
    public List<string> Statuses { get; set; }
    public int WorkflowTemplateId { get; set; }
    //public WorkflowTemplateVm WorkflowTemplate { get; set; }

    // Properties specific to DeferralPaymentForm
    public string? Number { get; set; }
    public string? KontrahentId { get; set; }
    public string? KontrahentName { get; set; }
    public bool? KontrahentPrzelew { get; set; }
    public bool? Przelew { get; set; }
    public string? Note { get; set; }
    public int? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public DateTime? Requested { get; set; }
    public List<ApprovalVm>? Approvals { get; set; }
    public List<OrganisationRoleForFormVm> Level1Approvers { get; set; }
    public List<OrganisationRoleForFormVm> Level2Approvers { get; set; }

    public string LVL1_EnovaEmpId { get; set; }
    public string LVL2_EnovaEmpId { get; set; }
    public string LVL1_EmployeeName { get; set; }
    public string LVL2_EmployeeName { get; set; }
    public bool isApproved { get; set; }
    public bool isApplied { get; set; }
    public string Numer_Fk { get; set; }
    public bool is_Firma { get; set; }
    public long Faktdoc_Id { get; set; }
    public int CC { get; set; }
    public string VATID { get; set; }
    public string RejectReason { get; set; } = string.Empty;
    public List<FormFileVm> FormFiles { get; set; }

    //public static readonly List<string> StatusValues = new List<string>
    //{
    //    "Rejestracja",
    //    "AprobalaL1",
    //    "AprobataL2",
    //    "Zakończone"
    //};

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DeferralPaymentForm, DeferralPaymentFormVm>()
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
             .ForMember(dest => dest.Statuses, opt => opt.MapFrom(src => src.Statuses.ToList())) // Ensure BusinessTravelStatusesx is a List<string>

             // Assuming Number is based on Id

             .ReverseMap();
        profile.CreateMap<DeferralPaymentFormVm, DeferralPaymentExportModel>().ReverseMap();
    }

    public DeferralPaymentFormVm()
    {
        Status = "Rejestracja";
        Statuses = GetDefaultStatuses();
        Note = string.Empty;
    }

    public static List<string> GetDefaultStatuses()
    {
        return new List<string>
            {
                "Rejestracja", "AprobataL1", "AprobataL2", "Zakończone", "Odrzucone"
            };
    }
}

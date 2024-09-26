
using Application.Common;
using Application.Mappings;
using Application.ViewModels;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Entities.ITWarehouse;
using Domain.Forms;
using Domain.WorkFlows;

namespace Application.Forms;

public class DeferralPaymentFormVm : IMapFrom<DeferralPaymentForm>
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
    public List<Approval>? Approvals { get; set;}
    public List<OrganisationRoleForFormVm> Level1Approvers { get; set;}
    public List<OrganisationRoleForFormVm> Level2Approvers { get; set;}

    public string LVL1_EnovaEmpId {  get; set; }
    public string LVL2_EnovaEmpId { get; set; }
    public string LVL1_EmployeeName { get; set; }
    public string LVL2_EmployeeName { get; set; }




    public void Mapping(Profile profile)
    {
       profile.CreateMap<DeferralPaymentForm, DeferralPaymentFormVm>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Statuses, opt => opt.MapFrom(src => src.Statuses.ToList())) // Ensure Statuses is a List<string>
            
            // Assuming Number is based on Id

            .ReverseMap();
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

using Application.Mappings;
using Application.ViewModels.General;
using AutoMapper;
using Domain.Forms;

namespace Application.Forms;

public class TestFormVm : IMapFrom<TestForm>
{
    // Properties from FormTemplate
    public int Id { get; set; }
    public string Name { get; set; } = "Formularz testowy";
    public string Description { get; set; } = "Formularz do testów";
    public string FolderName { get; set; } = "testForm";
    public string NumberPrefix { get; set; } = "TF";
    public string Status { get; set; }
    public List<string> Statuses { get; set; }
    public int WorkflowTemplateId { get; set; }

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
    public List<FormFileVm> FormFiles { get; set; }

    public string LVL1_EnovaEmpId { get; set; }
    public string LVL2_EnovaEmpId { get; set; }
    public string LVL1_EmployeeName { get; set; }
    public string LVL2_EmployeeName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TestForm, TestFormVm>()
             .ReverseMap();
    }

    public TestFormVm()
    {
        Status = "Rejestracja";
        Statuses = GetDefaultStatuses();
        Note = string.Empty;

    }

    public static List<string> GetDefaultStatuses()
    {
        return new List<string>
            {
                "Rejestracja", "AprobataL1", "AprobataL2", "Zakończone"
            };
    }
}

using Application.Interfaces;
using Application.Mappings;
using Application.ViewModels.CoC;
using Application.ViewModels.General;
using AutoMapper;
using Domain.Entities.Common;
using Domain.Forms;

namespace Application.Forms.CoC
{
    public class OnboardingFormVm : IMapFrom<OnboardingForm>, IFormVm
    {
        // Properties from FormTemplate
        public int Id { get; set; }
        public string Name { get; set; } = "Formularz Onboarding";
        public string Description { get; set; } = "Formularz do rejestrowania postępów w Onboardingu pracowników.";
        public string FolderName { get; set; } = "onboardingForm";
        public string NumberPrefix { get; set; } = "ONB";
        public string Status { get; set; }
        public List<string> Statuses { get; set; }
        public int WorkflowTemplateId { get; set; }
        public List<FormFile> FormFiles { get; set; }
        //public WorkflowTemplate WorkflowTemplate { get; set; }

        // Properties specific to Form flow and approval
        public string? Number { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? Requested { get; set; }
        public List<ApprovalVm>? Approvals { get; set; }
        public List<OrganisationRoleForFormVm> Level1Approvers { get; set; } // tu przypisać dziewczyny z Regionów które będą mogły coś z tym całym formularzem zrobić (nowa rola w Organizacjach - CoC Assistants)
        public List<OrganisationRoleForFormVm> Level2Approvers { get; set; } // tu przypisać Aleksandrę i Anię (nowa rola w Organizacjach - CoC Managers)
        public string LVL1_EnovaEmpId { get; set; }
        public string LVL2_EnovaEmpId { get; set; }
        public string LVL1_EmployeeName { get; set; }
        public string LVL2_EmployeeName { get; set; }
        public int ManagerId { get; set; }

        // Core Properties 
        public List<InstructionStatus> Instructions { get; set; }
        public string Group { get; set; }
        public string? Note { get; set; }
        public int? Progress { get; set; } = 0;
        public bool FirstRun { get; set; } = false;
        List<FormFileVm> IFormVm.FormFiles { get; set; }
        public DateTime? Modified { get; set; } = DateTime.Now;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OnboardingForm, OnboardingFormVm>()
                .ForMember(dest => dest.Approvals, opt => opt.Ignore())
                .ForMember(dest => dest.Level1Approvers, opt => opt.Ignore())
                .ForMember(dest => dest.Level2Approvers, opt => opt.Ignore())
                .ForMember(dest => dest.Instructions, opt => opt.Ignore())
                .ReverseMap();
        }
        public OnboardingFormVm()
        {
            Status = "Rejestracja";
            Statuses = GetDefaultStatuses();
        }

        public static List<string> GetDefaultStatuses()
        {
            return new List<string>
            {
                "Rejestracja", "W trakcie", "Zakończony"
            };
        }
    }

}

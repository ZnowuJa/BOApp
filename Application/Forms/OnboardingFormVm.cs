using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels.General;
using AutoMapper;

using Domain.Entities.CoC;
using Domain.Entities.Common;

namespace Application.Forms
{
    public class OnboardingFormVm
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
        public List<Approval>? Approvals { get; set; }
        public List<OrganisationRoleForFormVm> Level1Approvers { get; set; }
        public List<OrganisationRoleForFormVm> Level2Approvers { get; set; }
        public string LVL1_EnovaEmpId { get; set; }
        public string LVL2_EnovaEmpId { get; set; }
        public string LVL1_EmployeeName { get; set; }
        public string LVL2_EmployeeName { get; set; } 
        public int ManagerId { get; set; }

        // Core Properties 
        public List<InstructionCoC> InstructionCoC { get; set; }
        public string Group { get; set; }
        public string? Note { get; set; }
        public int? Progress { get; set; } = 0;
        public bool FirstRun { get; set; } = false;


        public void Mapping(Profile profile)
        {

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

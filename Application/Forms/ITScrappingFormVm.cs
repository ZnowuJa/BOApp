using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.WorkFlows;
using Application.ViewModels.General;
using Application.DTOs;
using Domain.Entities.ITWarehouse;

namespace Application.Forms;
public class ITScrappingFormVm
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

    public List<Approval>? Approvals { get; set; }
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Application.ViewModels;
using Application.ViewModels.General;

using Domain.Entities.Common;
using Domain.WorkFlows;

namespace Application.Forms;
public class ITSaleFormVm
{
    public int Id { get; set; }
    public string Name { get; set; } = "Sprzedaż sprzętu IT";
    public string Description { get; set; } = "Formularz do sprzedaży sprzętu IT";
    public string FolderName { get; set; } = "ITSaleForm";
    public string NumberPrefix { get; set; } = "ITSALE";
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
    public CompanyVm? Company { get; set; }
    public int EmployeeId {  get; set; }
    public EmployeeVm? Employee { get; set; }
    public ICollection<AssetDTO>? assets { get; set; }

}

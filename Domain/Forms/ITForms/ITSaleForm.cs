using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Common;
using Domain.Entities.Common;

namespace Domain.Forms.ITForms;
public class ITSaleForm : FormTemplate
{
    public ITSaleForm() : base("Formularz Zlomowania", "Formularz do rejestrowania złomowania sprzętu It", "ITscrappingForm", "ITS", "IT", "Rejestracja", 2)
    {
        Statuses = new List<string>
        {
            "Rejestracja", "W trakcie", "Zakończony" };
    }



    public string Approvals { get; set; }
    public string Level1Approvers { get; set; }
    public string Level2Approvers { get; set; }
    public string LVL1_EnovaEmpId { get; set; }
    public string LVL2_EnovaEmpId { get; set; }
    public string LVL1_EmployeeName { get; set; }
    public string LVL2_EmployeeName { get; set; }
    public List<FormFile> FormFiles { get; set; }
}

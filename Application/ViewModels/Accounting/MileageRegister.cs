using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms.Accounting;

namespace Application.ViewModels.Accounting;
public class MileageRegister
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeEmpId { get; set; }

    public string PrivateCarRegistration { get; set; }
    public string PrivateCarEngineSize { get; set; }
    public decimal MileageAllowanceRate { get; set; }
    public List<MileageRegisterEntry> Entries { get; set; }
    public decimal? TotalValue { get; set; }
    public decimal? TotalMileage { get; set; }

}


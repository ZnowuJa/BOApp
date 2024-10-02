using Application.ViewModels.General;
using System.Security.Claims;

namespace BOAppFluentUI.Components.Pages;

public class FormUserContext
{
    public ClaimsPrincipal? User { get; set; }
    public string LongName { get; set; }
    public EmployeeVm Employee { get; set; }
    public string EnovaEmpId { get; set; }
}

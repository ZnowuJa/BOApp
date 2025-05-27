using Application.ViewModels.General;
using System.Security.Claims;

namespace BOAppFluentUI.Components.Pages;

public class FormUserContext
{
    public ClaimsPrincipal? User { get; set; }
    public string LongName { get; set; }
    public EmployeeVm Employee { get; set; }
    public string EnovaEmpId { get; set; }
    public bool isFormAdmin { get; set; }
    public string AdminRole { get; set; }
    public string ITRole { get; set; }

    public FormUserContext(string adminRole, string itRole)
    {
        AdminRole = adminRole; //Admin role in form or application
        ITRole = itRole; // Role of IT department
    }
}

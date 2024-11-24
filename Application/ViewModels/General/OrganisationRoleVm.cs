namespace Application.ViewModels.General;
public class OrganisationRoleVm
{
    public bool IsDefault { get; set; }
    public int EmpId {  get; set; }
    public EmployeeVm Employee { get; set; }

    public OrganisationRoleVm()
    {
        Employee = new EmployeeVm();
    }
}

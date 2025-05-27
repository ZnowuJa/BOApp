using Application.ViewModels.General;

namespace Application.ViewModelsForForms;
public class EmployeeGridModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string LongName { get; set; }
    public string Email { get; set; }
    public int? ManagerId { get; set; }
    public ManagerVm Manager { get; set; }
    public EmployeeTypeVm? EmployeeTypeVm { get; set; }
    public string? DG { get; set; }
    public string? CC { get; set; }

}

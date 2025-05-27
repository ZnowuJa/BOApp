using Application.ViewModels.General;

namespace Application.ViewModelsForForms;
public class EmployeeEditModel
{
    public EmployeeVm employeeVm { get; set; }
    public ManagerVm managerVm { get; set; }

    public EmployeeEditModel(EmployeeVm _emp, ManagerVm _man)
    {
        employeeVm = _emp;
        managerVm = _man;

    }

}
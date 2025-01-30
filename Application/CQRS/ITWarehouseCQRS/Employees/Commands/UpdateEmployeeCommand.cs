using Application.ViewModels.General;

using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Commands;
public class UpdateEmployeeCommand : IRequest<int>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public int? ManagerId { get; set; }
    public EmployeeTypeVm? Type { get; set; }
    public string? DG { get; set; } = string.Empty;
    public string? CC { get; set; } = string.Empty;
    public int ? CocGroupId = 0;
    public EmployeeVm? EmployeeVm { get; set; }

    public UpdateEmployeeCommand(EmployeeVm? _employeeVm)
    {
        EmployeeVm = _employeeVm;
        Id = _employeeVm.Id;
    }

    public UpdateEmployeeCommand(
        int id, 
        string firstName, 
        string lastName, 
        string email,   
        string mobileNumber, 
        string phoneNumber, 
        int managerId, 
        EmployeeTypeVm type
        )
            {
                Id = id;
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                MobileNumber = mobileNumber;
                PhoneNumber = phoneNumber;
                ManagerId = managerId;
                Type = type;
            }
}
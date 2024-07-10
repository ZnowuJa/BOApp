using Application.ViewModels;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.ITWarehouseCQRS.Employees.Commands;
public class CreateEmployeeCommand : IRequest<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? IdentityUserId { get; set; }
    public string? IdentityUserName { get; set; }
    public string? ThirdPartyId { get; set; }
    public string? MobileNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public int ManagerId { get; set; }
    public bool IsManager { get; set; }
    public EmployeeTypeVm? Type { get; set; }
    public string? DG { get; set; } = string.Empty;
    public string? CC { get; set; } = string.Empty;

    public CreateEmployeeCommand( string firstName, string lastName, string email, string identityUserId, string identityUserName, string thirdPartyId, string mobileNumber, string phoneNumber, int managerId, bool isManager, EmployeeTypeVm type, string dG, string cC )
    {
        FirstName = firstName; 
        LastName = lastName;
        Email = email;
        IdentityUserId = identityUserId;
        IdentityUserName = identityUserName;
        ThirdPartyId = thirdPartyId;
        MobileNumber = mobileNumber;
        PhoneNumber = phoneNumber;
        ManagerId = managerId;
        IsManager = isManager;
        Type = type;
        DG = dG;
        CC = cC;

    }
}
using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Commands;
public class CreateEmployeeCommand : IRequest<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid AzureObjectId { get; set; }
    public string? IdentityUserName { get; set; }
    public string? ThirdPartyId { get; set; }
    public string? MobileNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public int ManagerId { get; set; }
    public bool IsManager { get; set; }
    public EmployeeTypeVm? Type { get; set; }
    public string? DG { get; set; } = string.Empty;
    public string? CC { get; set; } = string.Empty;

    public CreateEmployeeCommand( 
        string firstName, 
        string lastName, 
        string email, 
        Guid azureObjectId, 
        string identityUserName, 
        string thirdPartyId, 
        string mobileNumber, 
        string phoneNumber, 
        int managerId, 
        bool isManager, 
        EmployeeTypeVm type )
    {
        FirstName = firstName; 
        LastName = lastName;
        Email = email;
        AzureObjectId = azureObjectId;
        IdentityUserName = identityUserName;
        ThirdPartyId = thirdPartyId;
        MobileNumber = mobileNumber;
        PhoneNumber = phoneNumber;
        ManagerId = managerId;
        IsManager = isManager;
        Type = type;
    }
}
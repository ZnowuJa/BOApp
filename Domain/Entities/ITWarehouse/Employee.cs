using BackOfficeApp_Domain.Common;

using Domain.Interfaces;

namespace Domain.Entities.ITWarehouse;

public class Employee : AuditableEntity, IAssignee
{
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string LongName { get; set; }
    public string Email { get; set; }
    public Guid AzureObjectId { get; set; }
    public string? IdentityUserName { get; set; }
    public string? ThirdPartyId { get; set; }
    public int? EnovaEmpId { get; set; }
    public string? Position { get; set; }
    public string? MobileNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public int? ManagerId { get; set; }
    public bool IsManager { get; set; }
    public EmployeeType? Type{ get; set; }
    public int? IsActive { get; set; }
    public string? VcdCompanyNr { get; set; }
    public string? VcdempId { get; set; }
    public string? VcdempNumber { get; set; }
    public string? VcddeptNumber { get; set; }
    public string? SapNumber { get; set; }
    public string? FTEStartDate { get; set; }
    public string? FTEEndDate {  get; set; }
    public string? ManagerEmail { get; set; }  
    public string? DeptNumber {  get; set; }
    public string PersonalDeptNumber { get; set; }
    public string? AspNetUserId { get; set; }
    public string? Oeshort {  get; set; }
    public string? JobCode { get; set; }
    public int CoCGroupId { get; set; }
    public string? BankAccountNumber { get; set; }


    public Employee()
    {
        Id = 0;
        FirstName = "Select...";
        LastName = string.Empty;
        LongName = string.Empty;
        Email = string.Empty;
        AzureObjectId = Guid.Empty;
        IdentityUserName = string.Empty;
        ThirdPartyId = string.Empty;
        EnovaEmpId = 0;
        MobileNumber = string.Empty;
        PhoneNumber = string.Empty;
        ManagerId = 0;
        IsManager = false;
        Type = new EmployeeType();
        IsActive = 0;
        VcdCompanyNr = string.Empty;
        VcdempId = string.Empty;
        VcdempNumber = string.Empty;
        VcddeptNumber = string.Empty;
        SapNumber = string.Empty;
        FTEStartDate = string.Empty;
        FTEEndDate = string.Empty;
        ManagerEmail = string.Empty;
        DeptNumber = string.Empty;
        AspNetUserId = string.Empty;
        Oeshort = string.Empty;
        JobCode = string.Empty;
        CoCGroupId = 0;
        BankAccountNumber = string.Empty;
    }

}

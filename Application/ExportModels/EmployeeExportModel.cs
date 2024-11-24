namespace Application.ExportModels;
public class EmployeeExportModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string LongName { get; set; }
    public string Email { get; set; }

    public int EnovaEmpId { get; set; }
    public string? Position { get; set; }
    public string ManagerName { get; set; }
    public int ManagerId { get; set; }

    public bool IsManager { get; set; }

    public int StatusId { get; set; }
    public int? IsActive { get; set; }
    public string? VcdCompanyNr { get; set; }
    public string? VcdempId { get; set; }
    public string? VcdempNumber { get; set; }
    public string? VcddeptNumber { get; set; }
    public string SapNumber { get; set; }
    public string? FTEStartDate { get; set; }
    public string? FTEEndDate { get; set; }
    public string? ManagerEmail { get; set; }
    public string? DeptNumber { get; set; }
    public string? AspNetUserId { get; set; }
    public string Roles { get; set; }
    public string JobCode { get; set; }
    public int CoCGroupId { get; set; }
}


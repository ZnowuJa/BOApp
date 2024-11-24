namespace Application.ViewModels.General;
public class Employee2Select
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string LongName { get; set; }
    public string Email { get; set; }
    public Guid AzureObjectId { get; set; }
    public int? EnovaEmpId { get; set; }
    public string? Position { get; set; }

    public int? ManagerId { get; set; }
    public bool IsManager { get; set; }

    public int? IsActive { get; set; }

    public string? AspNetUserId { get; set; }

}

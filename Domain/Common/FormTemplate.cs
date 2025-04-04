using BackOfficeApp_Domain.Common;

namespace Domain.Common;

public abstract class FormTemplate : AuditableEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string? Number { get; set; } = "brak numeru"; //Just a next number/identifier of request
    public string OperationArea { get; private set; } //Accounting, IT, Settlements etc...
    public string? FolderName { get; private set; } //Folder name for storing files
    public string NumberPrefix { get; private set; } //Defined prefix for custom number of document
    // public string? Number { get; set; } = "brak numeru"; //Just a next number/identifier of request
    public string? Status { get; set; } //current status or step in workflow
    public List<string> Statuses { get; set; } //collection of possible statuses
    public int WorkflowTemplateId { get; set; }
    //public WorkflowTemplate WorkflowTemplate { get; set; }

    protected FormTemplate(string title, string description, string folderName, string numberPrefix, string operArea, string status, int workflowTemplateId)
    {
        Title = title;
        Description = description;
        FolderName = folderName;
        NumberPrefix = numberPrefix;
        OperationArea = operArea;
        Status = status;
        WorkflowTemplateId = workflowTemplateId;
    }
}
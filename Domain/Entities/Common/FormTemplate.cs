namespace Domain.Entities.Common;
public class FormTemplate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? TemplateArea { get; set;}
    public string? FolderName { get; set; }
    public string? Status { get; set;} //current status or step in workflow
    public IEnumerable<string> Statuses { get; set; }
}

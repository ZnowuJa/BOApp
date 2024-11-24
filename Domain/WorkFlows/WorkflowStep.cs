namespace Domain.WorkFlows;
public class WorkflowStep
{
    public int Id { get; set; }
    public int StepNumber { get; set; }
    public string WorkflowRole { get; set; } // values only from Organisation Dictionary
    public string Status { get; set; } // values only from Form Statuses
    public int WorkflowTemplateId {  get; set; }
    public WorkflowTemplate WorkflowTemplate { get; set; }
}

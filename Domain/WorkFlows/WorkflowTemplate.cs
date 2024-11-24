namespace Domain.WorkFlows;
public class WorkflowTemplate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FormClassName { get; set; }
    public IList<WorkflowStep> Steps { get; set; }
}

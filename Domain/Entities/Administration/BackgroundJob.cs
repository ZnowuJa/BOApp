namespace Domain.Entities.Administration;
public class BackgroundJob
{
    public int Id { get; set; }
    public string AssemblyName { get; set; }
    public string JobClass { get; set; }
    public string JobMethod { get; set; }
    public string CronExpression { get; set; }
    public bool Enabled { get; set; }
}
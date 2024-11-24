namespace Application.ViewModels.CoC;

public class InstructionForFormVm
{
    public int InstructionId { get; set; }
    public bool IsRead { get; set; }
    public string Title { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public DateOnly Published { get; set; }
    public string Link { get; set; }
    public string Colour { get; set; }
    public PriorityLevel Priority { get; set; }

}

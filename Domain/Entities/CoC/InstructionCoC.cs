using BackOfficeApp_Domain.Common;

namespace Domain.Entities.CoC;
public class InstructionCoC : SmallAuditableEntity
{
    public string Title { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public DateOnly Published { get; set; }
    public ICollection<GroupCoC> Groups { get; set; }
    public string Link { get; set; }
    public string Colour { get; set; }
    public int Priority { get; set; }
}

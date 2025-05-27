using Domain.Entities.ITWarehouse;

namespace Domain.Forms;

public class FormHistory
{
    public int Id { get; set; }
    public string FormNumber { get; set; }
    public DateTime ModifiedDate { get; set; }
    public int EnovaEmpId { get; set; }
    // public Employee ModifiedBy { get; set; }
    public string ModifiedFields { get; set; }
}
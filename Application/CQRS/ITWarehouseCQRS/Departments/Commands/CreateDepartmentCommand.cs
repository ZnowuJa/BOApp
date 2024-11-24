using MediatR;

namespace Application.ITWarehouseCQRS.Departments.Commands;
public class CreateDepartmentCommand : IRequest<int>
{
    
    public string DeptNumber { get; set; }
    public string LongName { get; set; }
    public int CompanyId { get; set; }
    public string CostCenter { get; set; }
    public string SapNumber { get; set; }
    public int WarehouseId { get; set; }
    public int ManagerEmpId { get; set; }
    public CreateDepartmentCommand(string number, string longname, int compId, string cc, string sapnumber, int whId, int manId)
    {
        DeptNumber = number;
        LongName = longname;
        CompanyId = compId;
        CostCenter = cc;
        SapNumber = sapnumber;
        WarehouseId = whId;
        ManagerEmpId = manId;
    }
}

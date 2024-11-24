using MediatR;

namespace Application.ITWarehouseCQRS.Departments.Commands;
public class UpdateDepartmentCommand : IRequest<int>
{
    public int Id { get; set; }
    public string DeptNumber { get; set; }
    public string DeptName { get; set; }
    public int CompanyId { get; set; }
    public string CostCenter { get; set; }
    public string SapNumber { get; set; }
    public int WarehouseId { get; set; }
    public int ManagerEmpId { get; set; }
    public UpdateDepartmentCommand(int id, string number, string name, int compId, string cc, string sapnumber, int whId, int manId)
    {
        Id = id;
        DeptNumber = number;
        DeptName = name;
        CompanyId = compId;
        CostCenter = cc;
        SapNumber = sapnumber;
        WarehouseId = whId;
        ManagerEmpId = manId;
    }
}

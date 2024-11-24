using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.ITWarehouseCQRS.Departments.Commands;
public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public CreateDepartmentCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        Department dept = new()
            {
                LongName = request.LongName,
                DeptNumber = request.DeptNumber,
                CompanyId = request.CompanyId,
                CostCenter = request.CostCenter,
                SapNumber = request.SapNumber,
                WarehouseId = request.WarehouseId,
                ManagerEmpId = request.ManagerEmpId,
            };
        _appDbContext.Departments.Add(dept);
        await _appDbContext.SaveChangesAsync();

        return dept.Id;
        
    }
}

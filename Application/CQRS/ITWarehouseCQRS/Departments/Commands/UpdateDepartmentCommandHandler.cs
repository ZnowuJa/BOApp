using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Departments.Commands;
public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public UpdateDepartmentCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var dept = await _appDbContext.Departments.Where(d => d.Id == request.Id).FirstOrDefaultAsync();
        dept.DeptNumber = request.DeptNumber;
        dept.LongName = request.DeptName;
        dept.CompanyId = request.CompanyId;
        dept.CostCenter = request.CostCenter;
        dept.SapNumber = request.SapNumber;
        dept.WarehouseId = request.WarehouseId;
        dept.ManagerEmpId   = request.ManagerEmpId;

        _appDbContext.Departments.Update(dept);
        await _appDbContext.SaveChangesAsync();
        return dept.Id;
    }
}

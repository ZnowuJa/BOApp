using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Departments.Commands;
public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, int>
{
    public IAppDbContext _appDbContext { get; }
    public DeleteDepartmentCommandHandler(IAppDbContext context)
    {
        _appDbContext = context;
    }

    

    public async Task<int> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var dept = await _appDbContext.Departments.Where(d => d.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Departments.Remove(dept);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        return dept.Id;
        
    }
}

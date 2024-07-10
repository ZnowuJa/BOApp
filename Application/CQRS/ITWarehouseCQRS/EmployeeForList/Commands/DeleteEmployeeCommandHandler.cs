using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Employees.Commands;
public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    public DeleteEmployeeCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Employees.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Employees.Remove(result);
        await _appDbContext.SaveChangesAsync();
        return result.Id;
    }
}

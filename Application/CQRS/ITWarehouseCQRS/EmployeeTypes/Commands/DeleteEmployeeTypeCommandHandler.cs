using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Commands;
public class DeleteEmployeeTypeCommandHandler : IRequestHandler<DeleteEmployeeTypeCommand, int>
{
    private readonly IAppDbContext _appDbContext;


    public DeleteEmployeeTypeCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
 
    }

    public async Task<int> Handle(DeleteEmployeeTypeCommand request, CancellationToken cancellationToken)
    {

        var item = await _appDbContext.EmployeeTypes.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.EmployeeTypes.Remove(item);
        await _appDbContext.SaveChangesAsync();
        //_logger.LogInformation("DeleteCTHandler : " + item.Id);
        return item.Id;
    }
}

using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Commands;
public class UpdateEmployeeTypeCommandHandler : IRequestHandler<UpdateEmployeeTypeCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public UpdateEmployeeTypeCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(UpdateEmployeeTypeCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var employeetype = await _appDbContext.EmployeeTypes.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        employeetype.Name = request.Name;
        await _appDbContext.SaveChangesAsync();
        return employeetype.Id;

    }
}

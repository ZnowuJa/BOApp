using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Warehouses.Commands;
public class UpdateWarehouseCommand(int id, int number, string name) : IRequest<int>
{
    public int Id { get; set; } = id;
    public int Number { get; set; } = number;
    public string Name { get; set; } = name;
}

public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public UpdateWarehouseCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var employeetype = await _appDbContext.Warehouses.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        employeetype.Name = request.Name;
        await _appDbContext.SaveChangesAsync();
        return employeetype.Id;

    }
}

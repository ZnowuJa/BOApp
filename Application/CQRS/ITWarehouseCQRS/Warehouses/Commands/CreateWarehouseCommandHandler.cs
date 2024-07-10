using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.ITWarehouseCQRS.Warehouses.Commands;
public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public CreateWarehouseCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        Warehouse employeetype = new()
        {
            Number = request.Number,
            Name = request.Name,
            StatusId = 1
        };
        _appDbContext.Warehouses.Add(employeetype);
        await _appDbContext.SaveChangesAsync();

        return employeetype.Id;
    }
}

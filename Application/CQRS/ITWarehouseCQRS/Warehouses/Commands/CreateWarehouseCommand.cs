using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Warehouses.Commands;
public class CreateWarehouseCommand(int number, string name) : IRequest<int>
{
    public int Number { get; set; } = number;
    public string Name { get; set; } = name;
}

public class CreateWarehouseCommandHandler(IAppDbContext appDbContext) : IRequestHandler<CreateWarehouseCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

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

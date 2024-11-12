using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Vendors.Commands;
public class CreateVendorCommand(string name) : IRequest<int>
{
    public string Name { get; set; } = name;
}
public class CreateVendorCommandHandler(IAppDbContext appDbContext) : IRequestHandler<CreateVendorCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

    public async Task<int> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        Vendor item = new()
        {
            Name = request.Name,
            StatusId = 1
        };
        _appDbContext.Vendors.Add(item);
        await _appDbContext.SaveChangesAsync();

        return item.Id;
    }
}

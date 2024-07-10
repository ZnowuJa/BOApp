using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.ITWarehouseCQRS.Vendors.Commands;
public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public CreateVendorCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

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

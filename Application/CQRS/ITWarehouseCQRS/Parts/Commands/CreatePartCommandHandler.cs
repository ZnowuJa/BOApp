using Application.Interfaces;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Parts.Commands;
public class CreatePartCommandHandler : IRequestHandler<CreatePartCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CreatePartCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreatePartCommand request, CancellationToken cancellationToken)
    {
        //var cat = await _appDbContext.Categories.Where(p => p.Id == request.CategoryVm.Id).FirstOrDefaultAsync();
        //var ven = await _appDbContext.Vendors.Where(p => p.Id == request.VendorVm.Id).FirstOrDefaultAsync();

        Part Part = new()
        {
            Name = request.Name,
            Category = await _appDbContext.Categories.Where(p => p.Id == request.CategoryVm.Id).FirstOrDefaultAsync(),
            Vendor = await _appDbContext.Vendors.Where(p => p.Id == request.VendorVm.Id).FirstOrDefaultAsync(),
            Description = request.Description,
            Photo = request.Photo,
            WarrantyPeriod = request.WarrantyPeriod,
            isCurrentlyOrderable = request.isCurrentlyOrderable,
            EndOfSupport = request.EndOfSupport
        };
        _appDbContext.Parts.Add(Part);
        await _appDbContext.SaveChangesAsync();
        return Part.Id;
    }
}

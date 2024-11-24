using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Parts.Commands;
public class UpdatePartCommandHandler : IRequestHandler<UpdatePartCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public UpdatePartCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdatePartCommand request, CancellationToken cancellationToken)
    {
        var cat = await _appDbContext.Categories.Where(p => p.Id == request.CategoryVm.Id).FirstOrDefaultAsync();
        var ven = await _appDbContext.Vendors.Where(p => p.Id == request.VendorVm.Id).FirstOrDefaultAsync();
        var item = await _appDbContext.Parts.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

        item.Name = request.Name;
        item.Category = cat;
        item.Vendor = ven;
        item.Description = request.Description;
        item.Photo = request.Photo;
        item.WarrantyPeriod = request.WarrantyPeriod;
        item.isCurrentlyOrderable = request.isCurrentlyOrderable;
        item.EndOfSupport = request.EndOfSupport;

        _appDbContext.Parts.Update(item);

        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}

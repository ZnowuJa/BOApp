using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Parts.Queries;
public class GetPartQueryHandler : IRequestHandler<GetPartQuery, PartVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mappper;
    public GetPartQueryHandler(IAppDbContext appDbContext, IMapper mappper)
    {
        _appDbContext = appDbContext;
        _mappper = mappper;
    }
    public async Task<PartVm> Handle(GetPartQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Parts.Where(p => p.Id == request.PartId)
            .Include(i => i.Vendor)
            .Include(i => i.Category)
            .FirstOrDefaultAsync();

        var res = _mappper.Map<PartVm>(result);

        return res;
    }
}
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Parts.Queries;
internal class GetAllPartsForSelectQueryHandler : IRequestHandler<GetAllPartsForSelectQuery, IQueryable<PartVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;

    public GetAllPartsForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }

    public async Task<IQueryable<PartVm>> Handle(GetAllPartsForSelectQuery request, CancellationToken cancellationToken)
    {
        Part item = new Part() { Id = 0, Name = "Select..."};
        List<Part> itemList = [item];
        var result = await _appDbContext.Parts
            .Where(p => p.StatusId == 1)
            .Include(i => i.Category)
            .Include(j => j.Vendor)
            .ToListAsync(cancellationToken);
        itemList.AddRange(result);
        var res = _mapper.Map<List<PartVm>>(itemList);
        return res.AsQueryable();
    }
}


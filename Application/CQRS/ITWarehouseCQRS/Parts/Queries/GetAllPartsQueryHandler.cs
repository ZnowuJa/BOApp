using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Parts.Queries;
public class GetAllPartsQueryHandler : IRequestHandler<GetAllPartsQuery, IQueryable<PartVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    public GetAllPartsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<IQueryable<PartVm>> Handle(GetAllPartsQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Parts.Where(p => p.StatusId == 1)
            .Include(i => i.Vendor)
            .Include(i => i.Category)
            .ToListAsync(cancellationToken);
        var res = _mapper.Map<List<PartVm>>(result);
        return res.AsQueryable();
    }
}
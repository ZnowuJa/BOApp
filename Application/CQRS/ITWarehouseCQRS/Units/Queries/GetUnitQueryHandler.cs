using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Units.Queries;
public class GetUnitQueryHandler : IRequestHandler<GetUnitQuery, UnitVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetUnitQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<UnitVm> Handle(GetUnitQuery request, CancellationToken cancellationToken)
    {
        var item = await _appDbContext.Units.Where(p => p.Id == request.UnitId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var itemVM = _mapper.Map<UnitVm>(item);
        return itemVM;
    }
}
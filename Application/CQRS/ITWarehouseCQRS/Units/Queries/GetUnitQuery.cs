using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Units.Queries;
public class GetUnitQuery(int i) : IRequest<UnitVm>
{
    public int UnitId { get; set; } = i;
}
public class GetUnitQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetUnitQuery, UnitVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public IMapper Mapper { get; }

    public async Task<UnitVm> Handle(GetUnitQuery request, CancellationToken cancellationToken)
    {
        var item = await _appDbContext.Units.Where(p => p.Id == request.UnitId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var itemVM = _mapper.Map<UnitVm>(item);
        return itemVM;
    }
}
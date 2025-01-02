using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Organisations.Queries;

public class GetLocationsQuery : IRequest<IEnumerable<Location>>
{
}

public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, IEnumerable<Location>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetLocationsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Location>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        var organisations = await _appDbContext.Organisations.Where(p => p.StatusId == 1) .GroupBy(o => o.SapNumber)
            .Select(g => g.First()).OrderBy(o => o.SapNumber).ToListAsync(cancellationToken);
        var locations = _mapper.Map<List<Location>>(organisations);

        return locations;
    }

}
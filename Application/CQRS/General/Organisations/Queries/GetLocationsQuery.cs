using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Organisations.Queries;

public class GetLocationsQuery : IRequest<IEnumerable<LocationVm>>
{
}

public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, IEnumerable<LocationVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetLocationsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LocationVm>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
    {
        var organisations = await _appDbContext.Organisations
            .Where(p => p.StatusId == 1)
            .ToListAsync(cancellationToken); // Materialize the query

        var uniqueOrganisations = organisations
            .GroupBy(o => o.SapNumber)
            .Select(g => g.First())
            .OrderBy(o => o.SapNumber)
            .ToList();
        var locations = _mapper.Map<List<LocationVm>>(uniqueOrganisations);

        return locations;
    }

}
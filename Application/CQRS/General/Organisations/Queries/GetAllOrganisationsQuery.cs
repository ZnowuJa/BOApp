using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Organisations.Queries;
public class GetAllOrganisationsQuery : IRequest<IQueryable<OrganisationVm>>
{
}


public class GetAllOrganisationsQueryHandler : IRequestHandler<GetAllOrganisationsQuery, IQueryable<OrganisationVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetAllOrganisationsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<IQueryable<OrganisationVm>> Handle(GetAllOrganisationsQuery request, CancellationToken cancellationToken)
    {
        var organisations = await _appDbContext.Organisations.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);
        
        var organisationVms = _mapper.Map<List<OrganisationVm>>(organisations);

        return organisationVms.AsQueryable();
    }
}
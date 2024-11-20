using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.ITWarehouseCQRS.CompanyTypes.Queries;
public class GetAllCompanyTypesQuery : IRequest<IQueryable<CompanyTypeVm>>
{
}
public class GetAllCompanyTypesQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllCompanyTypesQueryHandler> logger) : IRequestHandler<GetAllCompanyTypesQuery, IQueryable<CompanyTypeVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<CompanyTypeVm>> Handle(GetAllCompanyTypesQuery request, CancellationToken cancellationToken)
    {
        var curs = await _appDbContext.CompanyTypes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var curslist = _mapper.Map<List<CompanyTypeVm>>(curs);

        return curslist.AsQueryable();
    }
}
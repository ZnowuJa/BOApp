using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Companies.Queries;
public class GetAllCompaniesQuery : IRequest<IQueryable<CompanyVm>>
{
}
public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IQueryable<CompanyVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    public GetAllCompaniesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<IQueryable<CompanyVm>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Companies.Where(p => p.StatusId == 1).Include(i => i.CompanyType).ToListAsync(cancellationToken);
        var res = _mapper.Map<List<CompanyVm>>(result);
        return res.AsQueryable();
    }
}
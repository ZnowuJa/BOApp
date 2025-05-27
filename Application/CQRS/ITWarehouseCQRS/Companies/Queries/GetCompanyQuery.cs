using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Companies.Queries;
public class GetCompanyQuery(int i) : IRequest<CompanyVm>
{
    public int CompanyId { get; set; } = i;
}
public class GetCompanyQueryHandler(IAppDbContext appDbContext, IMapper mappper) : IRequestHandler<GetCompanyQuery, CompanyVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mappper = mappper;

    public async Task<CompanyVm> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Companies.Where(p => p.Id == request.CompanyId).Include(i => i.CompanyType).FirstOrDefaultAsync();
        var res = _mappper.Map<CompanyVm>(result);
        return res;
    }
}
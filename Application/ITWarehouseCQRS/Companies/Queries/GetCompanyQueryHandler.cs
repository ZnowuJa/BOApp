using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Companies.Queries;
public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mappper;
    public GetCompanyQueryHandler(IAppDbContext appDbContext, IMapper mappper)
    {
        _appDbContext = appDbContext;
        _mappper = mappper;
    }
    public async Task<CompanyVm> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Companies.Where(p => p.Id == request.CompanyId).Include(i => i.CompanyType).FirstOrDefaultAsync();
        var res = _mappper.Map<CompanyVm>(result);
        return res;
    }
}

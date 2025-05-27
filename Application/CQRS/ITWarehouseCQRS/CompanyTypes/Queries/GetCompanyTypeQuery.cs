using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.CompanyTypes.Queries;
public class GetCompanyTypeQuery(int i) : IRequest<CompanyTypeVm>
{
    public int CompanyTypeId { get; set; } = i;
}
public class GetCompanyTypeQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetCompanyTypeQuery, CompanyTypeVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public IMapper Mapper { get; }

    public async Task<CompanyTypeVm> Handle(GetCompanyTypeQuery request, CancellationToken cancellationToken)
    {
        var companytype = await _appDbContext.CompanyTypes.Where(p => p.Id == request.CompanyTypeId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var companytypeVM = _mapper.Map<CompanyTypeVm>(companytype);
        return companytypeVM;
    }
}
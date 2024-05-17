using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.CompanyTypes.Queries;
public class GetCompanyTypeQueryHandler : IRequestHandler<GetCompanyTypeQuery, CompanyTypeVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetCompanyTypeQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<CompanyTypeVm> Handle(GetCompanyTypeQuery request, CancellationToken cancellationToken)
    {
        var companytype = await _appDbContext.CompanyTypes.Where(p => p.Id == request.CompanyTypeId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var companytypeVM = _mapper.Map<CompanyTypeVm>(companytype);
        return companytypeVM;
    }
}

using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Companies.Queries;
public class GetAllCompaniesByTypeIdQuery(int id) : IRequest<IQueryable<CompanyVm>>
{
    public int TypeId { get; set; } = id;
}
public class GetAllCompaniesByTypeIdQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllCompaniesByTypeIdQuery, IQueryable<CompanyVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private IMapper _mapper = mapper;

    public async Task<IQueryable<CompanyVm>> Handle(GetAllCompaniesByTypeIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Companies.Where(p => p.StatusId == 1 && p.CompanyType.Id == request.TypeId).Include(i => i.CompanyType).ToListAsync(cancellationToken);
        var res = _mapper.Map<List<CompanyVm>>(result);
        return res.AsQueryable();
    }
}
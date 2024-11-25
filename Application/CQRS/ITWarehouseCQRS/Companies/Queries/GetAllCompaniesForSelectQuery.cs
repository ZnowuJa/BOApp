using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Companies.Queries;
public class GetAllCompaniesForSelectQuery : IRequest<IQueryable<CompanyVm>>
{
}
internal class GetAllCompaniesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllCompaniesForSelectQuery, IQueryable<CompanyVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private IMapper _mapper = mapper;

    public async Task<IQueryable<CompanyVm>> Handle(GetAllCompaniesForSelectQuery request, CancellationToken cancellationToken)
    {
        Company item = new Company() { Id = 0, Name = "Select..." };
        List<Company> itemList = [item];
        var result = await _appDbContext.Companies.Where(p => p.StatusId == 1)
                                                  .Include(i => i.CompanyType)
                                                  .ToListAsync(cancellationToken);
        itemList.AddRange(result);
        var res = _mapper.Map<List<CompanyVm>>(itemList);
        return res.AsQueryable();
    }
}
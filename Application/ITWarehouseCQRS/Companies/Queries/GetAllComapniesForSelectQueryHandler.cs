using Application.Interfaces;
using Application.ITWarehouseCQRS.Companies.Queries;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Comapnies.Queries;
internal class GetAllCompaniesForSelectQueryHandler : IRequestHandler<GetAllCompaniesForSelectQuery, IQueryable<CompanyVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;

    public GetAllCompaniesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }

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


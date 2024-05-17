using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CompanyTypes.Queries;
public class GetAllCompanyTypesForSelectQueryHandler : IRequestHandler<GetAllCompanyTypesForSelectQuery, IQueryable<CompanyTypeVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllCompanyTypesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllCompanyTypesForSelectQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<CompanyTypeVm>> Handle(GetAllCompanyTypesForSelectQuery request, CancellationToken cancellationToken)
    {
        List<CompanyType> itemsSelected = new();
        CompanyType itemFirst = new CompanyType() { Id = 0, Name = "Select CompanyType..." };
        itemsSelected.Add(itemFirst);
        var itemsFromDb = await _appDbContext.CompanyTypes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        itemsSelected.AddRange(itemsFromDb);
        var itemsList = _mapper.Map<List<CompanyTypeVm>>(itemsSelected);

        return itemsList.AsQueryable();
    }
}


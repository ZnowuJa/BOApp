using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CompanyTypes.Queries;
public class GetAllCompanyTypesQueryHandler : IRequestHandler<GetAllCompanyTypesQuery, IQueryable<CompanyTypeVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllCompanyTypesQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllCompanyTypesQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<CompanyTypeVm>> Handle(GetAllCompanyTypesQuery request, CancellationToken cancellationToken)
    {
        var curs = await _appDbContext.CompanyTypes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var curslist = _mapper.Map<List<CompanyTypeVm>>(curs);

        return curslist.AsQueryable();
    }

}


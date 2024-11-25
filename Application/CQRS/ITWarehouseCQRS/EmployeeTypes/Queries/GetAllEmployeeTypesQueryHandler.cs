using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.ITWarehouseCQRS.EmployeeTypes.Queries;
public class GetAllEmployeeTypesQueryHandler : IRequestHandler<GetAllEmployeeTypesQuery, IQueryable<EmployeeTypeVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllEmployeeTypesQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllEmployeeTypesQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<EmployeeTypeVm>> Handle(GetAllEmployeeTypesQuery request, CancellationToken cancellationToken)
    {
        var curs = await _appDbContext.EmployeeTypes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var curslist = _mapper.Map<List<EmployeeTypeVm>>(curs);

        return curslist.AsQueryable();
    }

}

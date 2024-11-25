using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.ITWarehouse;
using Application.ViewModels.General;


namespace Application.CQRS.ITWarehouseCQRS.EmployeeTypes.Queries;
public class GetAllEmployeeTypesForSelectQueryHandler : IRequestHandler<GetAllEmployeeTypesForSelectQuery, IQueryable<EmployeeTypeVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllEmployeeTypesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllEmployeeTypesForSelectQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<EmployeeTypeVm>> Handle(GetAllEmployeeTypesForSelectQuery request, CancellationToken cancellationToken)
    {
        List<EmployeeType> itemsSelected = new();
        EmployeeType itemFirst = new EmployeeType() { Id = 0, Name = "Select EmployeType..." };
        itemsSelected.Add(itemFirst);
        var itemsFromDb = await _appDbContext.EmployeeTypes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        itemsSelected.AddRange(itemsFromDb);
        var itemsList = _mapper.Map<List<EmployeeTypeVm>>(itemsSelected);

        return itemsList.AsQueryable();
    }
}

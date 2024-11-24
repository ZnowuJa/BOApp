using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Entities.ITWarehouse;

namespace Application.ITWarehouseCQRS.CategoryTypes.Queries;
public class GetAllCategoryTypesForSelectQueryHandler : IRequestHandler<GetAllCategoryTypesForSelectQuery, IQueryable<CategoryTypeVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllCategoryTypesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllCategoryTypesForSelectQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<CategoryTypeVm>> Handle(GetAllCategoryTypesForSelectQuery request, CancellationToken cancellationToken)
    {
        List<CategoryType> ctsSelect = new();
        CategoryType firstCt = new CategoryType() { Id = 0, Name = "Select Category EmployeeTypeVm..." };
        ctsSelect.Add(firstCt);
        var cts =  await _appDbContext.CategoryTypes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        ctsSelect.AddRange(cts);
        var ctslist = _mapper.Map<List<CategoryTypeVm>>(ctsSelect);

        return ctslist.AsQueryable();
    }

}

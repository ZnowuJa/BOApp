using MediatR;
using Application.ViewModels;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.CategoryTypes.Queries;
public class GetAllCategoryTypesForSelectQuery : IRequest<IQueryable<CategoryTypeVm>>
{
}
public class GetAllCategoryTypesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllCategoryTypesForSelectQueryHandler> logger) : IRequestHandler<GetAllCategoryTypesForSelectQuery, IQueryable<CategoryTypeVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<CategoryTypeVm>> Handle(GetAllCategoryTypesForSelectQuery request, CancellationToken cancellationToken)
    {
        List<CategoryType> ctsSelect = new();
        CategoryType firstCt = new CategoryType() { Id = 0, Name = "Select Category EmployeeTypeVm..." };
        ctsSelect.Add(firstCt);
        var cts = await _appDbContext.CategoryTypes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        ctsSelect.AddRange(cts);
        var ctslist = _mapper.Map<List<CategoryTypeVm>>(ctsSelect);

        return ctslist.AsQueryable();
    }

}
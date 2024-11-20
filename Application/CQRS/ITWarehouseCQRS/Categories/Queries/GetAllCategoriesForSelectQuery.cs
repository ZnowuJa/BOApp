using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Categories.Queries;
public class GetAllCategoriesForSelectQuery : IRequest<IQueryable<CategoryVm>>
{
}
public class GetAllCategoriesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllCategoriesForSelectQuery, IQueryable<CategoryVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private IMapper _mapper = mapper;

    public async Task<IQueryable<CategoryVm>> Handle(GetAllCategoriesForSelectQuery request, CancellationToken cancellationToken)
    {
        Category cat = new Category() { Id = 0, Name = "Select...", LeadingZeros = 0 };
        List<Category> catsList = [cat];
        //catsList.Add(cat);
        var result = await _appDbContext.Categories.Where(p => p.StatusId == 1).Include(i => i.CategoryType).ToListAsync(cancellationToken);
        catsList.AddRange(result);
        var res = _mapper.Map<List<CategoryVm>>(catsList);
        return res.AsQueryable();
    }

}
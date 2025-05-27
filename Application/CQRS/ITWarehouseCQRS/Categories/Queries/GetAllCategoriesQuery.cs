using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Categories.Queries;
public class GetAllCategoriesQuery : IRequest<IQueryable<CategoryVm>>
{
}
public class GetAllCategoriesQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, IQueryable<CategoryVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private IMapper _mapper = mapper;

    public async Task<IQueryable<CategoryVm>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {


        var result = await _appDbContext.Categories.Where(p => p.StatusId == 1).Include(i => i.CategoryType).ToListAsync(cancellationToken);
        var res = _mapper.Map<List<CategoryVm>>(result);
        //Console.WriteLine("Handler result" + res.Count);
        return res.AsQueryable();
    }

}
using Application.Interfaces;
using Application.ITWarehouseCQRS.States.Queries;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Categories.Queries;
internal class GetAllCategoriesForSelectQueryHandler : IRequestHandler<GetAllCategoriesForSelectQuery, IQueryable<CategoryVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;

    public GetAllCategoriesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }

    public async Task<IQueryable<CategoryVm>> Handle(GetAllCategoriesForSelectQuery request, CancellationToken cancellationToken)
    {
        Category cat = new Category() { Id = 0, Name = "Select..."};
        List<Category> catsList = [cat];
        //catsList.Add(cat);
        var result = await _appDbContext.Categories.Where(p => p.StatusId == 1).Include(i => i.CategoryType).ToListAsync(cancellationToken);
        catsList.AddRange(result);
        var res = _mapper.Map<List<CategoryVm>>(catsList);
        return res.AsQueryable();
    }

}


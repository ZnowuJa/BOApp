﻿using Application.Interfaces;
using Application.ITWarehouseCQRS.CategoryTypes;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Categories.Queries;
public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IQueryable<CategoryVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    public GetAllCategoriesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<IQueryable<CategoryVm>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {


        var result = await _appDbContext.Categories.Where(p => p.StatusId == 1).Include(i => i.CategoryType).ToListAsync(cancellationToken);
        var res = _mapper.Map<List<CategoryVm>>(result);
        //Console.WriteLine("Handler result" + res.Count);
        return res.AsQueryable();
    }
    
}

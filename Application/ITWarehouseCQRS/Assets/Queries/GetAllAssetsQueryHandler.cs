﻿using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Assets.Queries;
public class GetAllAssetsQueryHandler : IRequestHandler<GetAllAssetsQuery, IQueryable<AssetVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    public GetAllAssetsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<IQueryable<AssetVm>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Assets.Where(p => p.StatusId == 1)
            .ToListAsync(cancellationToken);
        var res = _mapper.Map<List<AssetVm>>(result);
        
        return res.AsQueryable();
    }
}
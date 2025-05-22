using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using Domain.Entities.Accounting;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.Countries.Queries
{
    public class GetAllCountryForSelectQuery : IRequest<IQueryable<CountryVm>> { }

    public class GetAllCountryForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllCountryForSelectQuery, IQueryable<CountryVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<CountryVm>> Handle(GetAllCountryForSelectQuery request, CancellationToken cancellationToken)
        {
            var itemsFromDb = await _appDbContext.Countries
                .Where(c => c.StatusId == 1 && !string.IsNullOrEmpty(c.Name))
                .AsNoTracking()
                .OrderBy(c => c.Name).ToListAsync(cancellationToken);

            var itemsList = _mapper.Map<List<CountryVm>>(itemsFromDb);

            return itemsList.AsQueryable();
        }
    }
}
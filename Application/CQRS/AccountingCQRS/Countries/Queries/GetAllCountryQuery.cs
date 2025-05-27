using Application.CQRS.ITWarehouseCQRS.Currencies.Queries;
using Application.DTOs;
using Application.Interfaces;
using Application.ViewModels.General;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.ViewModels;
using Domain.Entities.Accounting;

namespace Application.CQRS.AccountingCQRS.Countries.Queries
{
    public class GetAllCountryQuery : IRequest<IQueryable<CountryVm>>
    {
    }
    public class GetAllCountryQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllCountryQuery, IQueryable<CountryVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<CountryVm>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {
            var listItems = new List<CountryVm>();
            var currencies = await _appDbContext.Currencies.ToListAsync();
            List<Country>? countries = await _appDbContext.Countries
                                               .Where(ct => ct.StatusId == 1)
                                               .ToListAsync(cancellationToken);
            foreach (var item in countries)
            {
                var currency = currencies.FirstOrDefault(p => p.Id == item.CurrencyId);
                var newItem = new CountryVm();

                newItem.CurrencyVmId = currency?.Id ?? 0;
                newItem.CurrencyVmName = currency?.Name ?? string.Empty;
                newItem.currencyVm = _mapper.Map<CurrencyVm>(currency);

                newItem.Id = item.Id;
                newItem.CountryCode = item.CountryCode;
                newItem.Name = item.Name;
                newItem.IsEU = item.IsEU;
                newItem.IsPL = item.IsPL;
                // newItem.CurrencyId = item.CurrencyId;
                newItem.Allowance = item.Allowance;
                newItem.TravelAllowance = item.TravelAllowance;
                //newItem.LocalTravelAllowance = item.LocalTravelAllowance;
                newItem.MaxHotelCost = item.MaxHotelCost;


                listItems.Add(newItem);
            }
            return listItems.AsQueryable();
        }
    }
}

using Application.Forms;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.AccountingCQRS.AccountingNote.Queries
{
    public class GetAllAccountingNoteQuery : IRequest<IQueryable<AccountingNoteFormVm>>
    {
    }
    public class GetAllAccountingNoteQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllAccountingNoteQueryHandler> logger) : IRequestHandler<GetAllAccountingNoteQuery, IQueryable<AccountingNoteFormVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger _logger = logger;

        public async Task<IQueryable<AccountingNoteFormVm>> Handle(GetAllAccountingNoteQuery request, CancellationToken cancellationToken)
        {
            var listItems = new List<AccountingNoteFormVm>();
            var companies = await _appDbContext.Companies.ToListAsync(cancellationToken);
            var accountingNotes = await _appDbContext.AccountingNotes
                                                     .Where(ct => ct.StatusId == 1)
                                                     .AsNoTracking()
                                                     .ToListAsync(cancellationToken);
            //var accountingNoteVms = _mapper.Map<List<AccountingNoteFormVm>>(accountingNotes);

            //return accountingNoteVms.AsQueryable();

            foreach (var item in accountingNotes)
            {
                //var company = companies.FirstOrDefault(p => p.Id == item.CompanyId);
                //var newItem = new CountryVm();

                //newItem.CurrencyVmId = currency?.Id ?? 0;
                //newItem.CurrencyVmName = currency?.Title ?? string.Empty;
                //newItem.currencyVm = _mapper.Map<CurrencyVm>(currency);

                //newItem.Id = item.Id;
                //newItem.CountryCode = item.CountryCode;
                //newItem.Title = item.Title;
                //newItem.Title = item.Title;
                //newItem.IsEU = item.IsEU;
                //newItem.IsPL = item.IsPL;
                //// newItem.CurrencyId = item.CurrencyId;
                //newItem.Allowance = item.Allowance;
                //newItem.AllowanceFirstDay8H = item.AllowanceFirstDay8H;
                //newItem.AllowanceFirstDay12H = item.AllowanceFirstDay12H;
                //newItem.AllowanceNextDay8H = item.AllowanceNextDay8H;
                //newItem.AllowanceNextDay12H = item.AllowanceNextDay12H;
                //newItem.BreakfastReduction = item.BreakfastReduction;
                //newItem.LunchReduction = item.LunchReduction;
                //newItem.DinnerReduction = item.DinnerReduction;
                //newItem.AccomodationAllowance = item.AccomodationAllowance;
                //newItem.TravelAllowance = item.TravelAllowance;
                //newItem.LocalTravelAllowance = item.LocalTravelAllowance;


                //listItems.Add(newItem);
            }
            return listItems.AsQueryable();
        }
    }
}
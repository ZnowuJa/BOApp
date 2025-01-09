using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Invoices.Queries;
public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, IQueryable<InvoiceVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    public GetAllInvoicesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<IQueryable<InvoiceVm>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        var listItem = new List<InvoiceVm>();
        var companies = await _appDbContext.Companies.ToListAsync(cancellationToken);
        var currencies = await _appDbContext.Currencies.ToListAsync(cancellationToken);

        var result = await _appDbContext.Invoices.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);

        foreach (var item in result)
        {
            var company = companies.FirstOrDefault(p => p.Id == item.CompanyId);
            var currency = currencies.FirstOrDefault(p => p.Id == item.CurrencyId);

            var resVm = new InvoiceVm()
            {
                Id = item.Id,
                Number = item.Number,
                CompanyVmId = company?.Id ?? 0,
                CompanyVmName = company?.Name ?? string.Empty,
                CompanyVm = _mapper.Map<CompanyVm>(company),
                Date = item.Date,
                TotalNet = item.TotalNet,
                CurrencyVmId = currency?.Id ?? 0,
                CurrencyVmName = currency?.Name ?? string.Empty,
                CurrencyVm = _mapper.Map<CurrencyVm>(currency)
            };
            listItem.Add(resVm);
        }
        //var result = await _appDbContext.Invoices.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);
        //foreach ( var item in result )
        //{
        //    var resCompanyVm = _mapper.Map<CompanyVm>(await _appDbContext.Companies.Where(p => p.Id == item.CompanyId).FirstOrDefaultAsync());
        //    var resCurrencyVm = _mapper.Map<currencyVm>(await _appDbContext.Currencies.Where(p => p.Id == item.CurrencyId).FirstOrDefaultAsync());
        //    var resVm = new InvoiceVm()
        //    {
        //        Id = item.Id,
        //        Number = item.Number,
        //        CompanyVmId = resCompanyVm.Id,
        //        CompanyVmName = resCompanyVm.Title,
        //        CompanyVm = resCompanyVm,
        //        Date = item.Date,
        //        TotalNet = item.TotalNet,
        //        CurrencyVmId = resCurrencyVm.Id,
        //        CurrencyVmName = resCurrencyVm.Title,
        //        currencyVm = resCurrencyVm
        //    };
        //    listItem.Add(resVm);
        //}

        //var res = _mapper.Map<List<InvoiceVm>>(result);
        return listItem.AsQueryable();
    }
}

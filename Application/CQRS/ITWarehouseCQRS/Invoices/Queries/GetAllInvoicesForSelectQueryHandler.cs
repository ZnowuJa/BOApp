using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Invoices.Queries;
internal class GetAllInvoicesForSelectQueryHandler : IRequestHandler<GetAllInvoicesForSelectQuery, IQueryable<InvoiceVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;

    public GetAllInvoicesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }

    public async Task<IQueryable<InvoiceVm>> Handle(GetAllInvoicesForSelectQuery request, CancellationToken cancellationToken)
    {
        InvoiceVm inv = new InvoiceVm() { Id = 0, Number = "Select..."};
        List<InvoiceVm> listItem = [inv];
        var result = await _appDbContext.Invoices.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);
        foreach (var item in result)
        {
            var resCompanyVm = _mapper.Map<CompanyVm>(await _appDbContext.Companies.Where(p => p.Id == item.CompanyId).FirstOrDefaultAsync());
            var resCurrencyVm = _mapper.Map<CurrencyVm>(await _appDbContext.Currencies.Where(p => p.Id == item.CurrencyId).FirstOrDefaultAsync());
            var resVm = new InvoiceVm()
            {
                Id = item.Id,
                Number = item.Number,
                CompanyVmId = resCompanyVm.Id,
                CompanyVmName = resCompanyVm.Name,
                CompanyVm = resCompanyVm,
                Date = item.Date,
                TotalNet = item.TotalNet,
                CurrencyVmId = resCurrencyVm.Id,
                CurrencyVmName = resCurrencyVm.Name,
                CurrencyVm = resCurrencyVm
            };
            listItem.Add(resVm);
        }

        return listItem.AsQueryable();
    }
}


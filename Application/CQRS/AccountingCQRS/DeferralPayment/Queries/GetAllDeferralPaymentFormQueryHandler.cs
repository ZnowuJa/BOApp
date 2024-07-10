using Application.Forms;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;

using Domain.Forms;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetAllDeferralPaymentFormQueryHandler : IRequestHandler<GetAllDeferralPaymentFormQuery, IQueryable<DeferralPaymentFormVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllDeferralPaymentFormQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllDeferralPaymentFormQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<DeferralPaymentFormVm>> Handle(GetAllDeferralPaymentFormQuery request, CancellationToken cancellationToken)
    {
        var dpmnts = await _appDbContext.DeferralPayments.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var dpmntslist = _mapper.Map<List<DeferralPaymentFormVm>>(dpmnts);

        return dpmntslist.AsQueryable();
    }

}

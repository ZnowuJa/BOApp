using Application.Forms;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetDeferralPaymentQueryHandler : IRequestHandler<GetDeferralPaymentQuery, DeferralPaymentFormVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetDeferralPaymentQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<DeferralPaymentFormVm> Handle(GetDeferralPaymentQuery request, CancellationToken cancellationToken)
    {
        //var temp = new DeferralPaymentFormVm();
        var item = await _appDbContext.DeferralPayments.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        var itemVM = _mapper.Map<DeferralPaymentFormVm>(item);
        return itemVM;
        //return temp;
    }
}
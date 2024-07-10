using System.Text.Json;

using Application.Forms;
using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms;

using MediatR;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Commands;
public class CreateDeferralPaymentCommandHandler : IRequestHandler<CreateDeferralPaymentCommand, DeferralPaymentFormVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateDeferralPaymentCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<DeferralPaymentFormVm> Handle(CreateDeferralPaymentCommand request, CancellationToken cancellationToken)
    {
        DeferralPaymentFormVm vm = new DeferralPaymentFormVm();

        using var transaction = await _appDbContext.BeginTransactionAsync();
        try
        {
            var item = _mapper.Map<DeferralPaymentForm>(request.Item);
            item.Approvals = SerializeApprovals(request.Item.Approvals);
            _appDbContext.DeferralPayments.Add(item);
            await _appDbContext.SaveChangesAsync();

            item.Number = $"{item.NumberPrefix}{item.Id.ToString("D8")}";
            item.StatusId = 1;
            _appDbContext.DeferralPayments.Update(item);
            item.Requested = DateTime.Now;
            await _appDbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            vm = _mapper.Map<DeferralPaymentFormVm>(item);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        
        return vm;
    }
    private string SerializeApprovals(List<Approval> approvals)
    {
        return approvals == null || approvals.Count == 0 ? null : JsonSerializer.Serialize(approvals);
    }
}

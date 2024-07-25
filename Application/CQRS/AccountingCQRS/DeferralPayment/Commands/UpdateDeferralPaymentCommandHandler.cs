using System.Text.Json;

using Application.Forms;
using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms;

using MediatR;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Commands;
public class UpdateDeferralPaymentCommandHandler : IRequestHandler<UpdateDeferralPaymentCommand, DeferralPaymentFormVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public UpdateDeferralPaymentCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<DeferralPaymentFormVm> Handle(UpdateDeferralPaymentCommand request, CancellationToken cancellationToken)
    {
        
        using var transaction = await _appDbContext.BeginTransactionAsync();
        try
        {
            var item = _mapper.Map<DeferralPaymentForm>(request.Item);
            item.Approvals = SerializeApprovals(request.Item.Approvals);
            item.Level1Approvers = SerializeRole(request.Item.Level1Approvers);
            item.Level2Approvers = SerializeRole(request.Item.Level2Approvers);

            _appDbContext.DeferralPayments.Add(item);
            await _appDbContext.SaveChangesAsync();

            await transaction.CommitAsync();
            
            
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return request.Item;

    }

    private string SerializeApprovals(List<Approval> approvals)
    {
        return approvals == null || approvals.Count == 0 ? null : JsonSerializer.Serialize(approvals);
    }

    private string SerializeRole(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? null : JsonSerializer.Serialize(roles);
    }

}

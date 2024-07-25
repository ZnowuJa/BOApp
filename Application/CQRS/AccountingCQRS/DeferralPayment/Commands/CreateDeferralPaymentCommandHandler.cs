using System.Text.Json;

using Application.Forms;
using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms;

using MediatR;

using Microsoft.EntityFrameworkCore;

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
        using var transaction = await _appDbContext.BeginTransactionAsync();
        try
        {
            var item = _mapper.Map<DeferralPaymentForm>(request.Item);
            item.Approvals = SerializeApprovals(request.Item.Approvals);
            item.Level1Approvers = SerializeRole(request.Item.Level1Approvers);
            item.Level2Approvers = SerializeRole(request.Item.Level2Approvers);
            //// Check if the WorkflowTemplate is already being tracked
            //var trackedWorkflowTemplate = await _appDbContext.WorkflowTemplates
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(wt => wt.Id == item.WorkflowTemplateId);

            //if (trackedWorkflowTemplate != null)
            //{
            //    // Attach the existing instance to avoid tracking issues
            //    _appDbContext.WorkflowTemplates.Attach(trackedWorkflowTemplate);
            //    item.WorkflowTemplate = trackedWorkflowTemplate;
            //}
            _appDbContext.DeferralPayments.Add(item);
            await _appDbContext.SaveChangesAsync();

            item.Number = $"{item.NumberPrefix}{item.Id.ToString("D8")}";
            item.StatusId = 1;
            _appDbContext.DeferralPayments.Update(item);
            item.Requested = DateTime.Now;
            await _appDbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            request.Item.Number = item.Number;
            request.Item.Status = item.Status;
            request.Item.Requested = item.Requested;

        }
        catch (Exception ex)
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

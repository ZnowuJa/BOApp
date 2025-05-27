using Application.Forms;
using Application.Forms.Accounting;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetAllDeferralPaymentFormQuery : IRequest<IQueryable<DeferralPaymentFormVm>>
{
}
public class GetAllDeferralPaymentFormQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllDeferralPaymentFormQueryHandler> logger) : IRequestHandler<GetAllDeferralPaymentFormQuery, IQueryable<DeferralPaymentFormVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<DeferralPaymentFormVm>> Handle(GetAllDeferralPaymentFormQuery request, CancellationToken cancellationToken)
    {
        var dpmntsList = new List<DeferralPaymentFormVm>();

        var dpmnts = await _appDbContext.DeferralPayments.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        foreach (var model in dpmnts)
        {
            //var itemVM = _mapper.Map<DeferralPaymentFormVm>(dpmnt);

            var itemVm = new DeferralPaymentFormVm
            {
                Id = model.Id,
                Name = model.Title,
                Description = model.Description,
                FolderName = model.FolderName,
                NumberPrefix = model.NumberPrefix,
                Status = model.Status,
                Number = model.Number,
                KontrahentId = model.KontrahentId,
                KontrahentName = model.KontrahentName,
                KontrahentPrzelew = model.KontrahentPrzelew,
                Przelew = model.Przelew,
                Note = model.Note,
                EmployeeId = model.EmployeeId,
                EmployeeName = model.EmployeeName,
                Requested = model.Requested,
                LVL1_EnovaEmpId = model.LVL1_EnovaEmpId,
                LVL2_EnovaEmpId = model.LVL2_EnovaEmpId,
                LVL1_EmployeeName = model.LVL1_EmployeeName,
                LVL2_EmployeeName = model.LVL2_EmployeeName,
                Approvals = DeserializeApprovals(model.Approvals),
                Level1Approvers = DeserializeRoles(model.Level1Approvers),
                Level2Approvers = DeserializeRoles(model.Level2Approvers),
                isApproved = model.isApproved,
                isApplied = model.isApplied,
                Numer_Fk = model.Numer_Fk,
                is_Firma = model.is_Firma,
                Faktdoc_Id = model.Faktdoc_Id,
                CC = model.CC,
                VATID = model.VATID
            };

            dpmntsList.Add(itemVm);
        }

        //var dpmntslist = _mapper.Map<List<DeferralPaymentFormVm>>(dpmnts);

        return dpmntsList.AsQueryable();
    }
    private List<ApprovalVm> DeserializeApprovals(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<ApprovalVm>() : JsonSerializer.Deserialize<List<ApprovalVm>>(json);
    }
    private List<OrganisationRoleForFormVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleForFormVm>() : JsonSerializer.Deserialize<List<OrganisationRoleForFormVm>>(json);
    }
}
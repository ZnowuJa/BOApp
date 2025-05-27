using Application.Forms;
using Application.Forms.Accounting;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetDeferralPaymentQuery(int i) : IRequest<DeferralPaymentFormVm>
{
    public int Id { get; set; } = i;
}

public class GetDeferralPaymentQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetDeferralPaymentQuery, DeferralPaymentFormVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<DeferralPaymentFormVm> Handle(GetDeferralPaymentQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine();
        var model = await _appDbContext.DeferralPayments.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
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
            WorkflowTemplateId = model.WorkflowTemplateId,
            isApproved = model.isApproved,
            isApplied = model.isApplied,
            Numer_Fk = model.Numer_Fk,
            is_Firma = model.is_Firma,
            Faktdoc_Id = model.Faktdoc_Id,
            CC = model.CC,
            VATID = model.VATID
        };

        return itemVm;
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
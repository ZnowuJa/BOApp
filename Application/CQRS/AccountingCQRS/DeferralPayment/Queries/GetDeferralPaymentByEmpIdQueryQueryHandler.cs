using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Forms;
using Application.ViewModels.General;
using System.Text.Json;
using Application.Forms.Accounting;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetDeferralPaymentByEmpIdQueryQueryHandler : IRequestHandler<GetDeferralPaymentByEmpIdQuery, IQueryable<DeferralPaymentFormVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetDeferralPaymentByEmpIdQueryQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<IQueryable<DeferralPaymentFormVm>> Handle(GetDeferralPaymentByEmpIdQuery request, CancellationToken cancellationToken)
    {
        var empId = int.Parse(request.Id);
        var result = await _appDbContext.DeferralPayments.Where(p => p.EmployeeId == empId).ToListAsync(cancellationToken);

        var items = new List<DeferralPaymentFormVm>();

        foreach (var item in result)
        {
            items.Add(MapToViewModel(item));
        }
        
        //itemVM;
        //return temp;

        return items.AsQueryable();
    }

    private DeferralPaymentFormVm MapToViewModel(DeferralPaymentForm model)
    {
        var item = new DeferralPaymentFormVm
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
        
        return item;
    }

    private List<Approval> DeserializeApprovals(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<Approval>() : JsonSerializer.Deserialize<List<Approval>>(json);
    }

    private List<OrganisationRoleForFormVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleForFormVm>() : JsonSerializer.Deserialize<List<OrganisationRoleForFormVm>>(json);
    }
}

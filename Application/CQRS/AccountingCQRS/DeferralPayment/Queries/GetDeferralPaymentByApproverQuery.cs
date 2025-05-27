using Application.Forms;

using MediatR;
using System.Text.Json;
using AutoMapper;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Forms;
using Application.ViewModels.General;
using Application.Forms.Accounting;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetDeferralPaymentByApproverQuery(string i) : IRequest<IQueryable<DeferralPaymentFormVm>>
{
    public string Id { get; set; } = i;
}

public class GetDeferralPaymentByApproverQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetDeferralPaymentByApproverQuery, IQueryable<DeferralPaymentFormVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public IMapper Mapper { get; }

    public async Task<IQueryable<DeferralPaymentFormVm>> Handle(GetDeferralPaymentByApproverQuery request, CancellationToken cancellationToken)
    {
        var query = $@"
        SELECT * 
        FROM DeferralPayments
        WHERE 
            EXISTS (
                SELECT 1
                FROM OPENJSON(Level1Approvers)
                WITH (EmpId int '$.EmpId') AS json
                WHERE json.EmpId = {request.Id}
            )
            OR 
            EXISTS (
                SELECT 1
                FROM OPENJSON(Level2Approvers)
                WITH (EmpId int '$.EmpId') AS json
                WHERE json.EmpId = {request.Id}
            )";

        //var empIdParameter = new SqlParameter("@empId", request.Id);

        var result = await _appDbContext.DeferralPayments.FromSqlRaw(query).ToListAsync(cancellationToken);


        //var temp = new DeferralPaymentFormVm();
        //var item = await _appDbContext.DeferralPayments
        //    .Where(p => p.LVL1_EnovaEmpId == request.Id || p.LVL2_EnovaEmpId == request.Id)
        //    .ToListAsync(cancellationToken);
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

    private List<ApprovalVm> DeserializeApprovals(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<ApprovalVm>() : JsonSerializer.Deserialize<List<ApprovalVm>>(json);
    }

    private List<OrganisationRoleForFormVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleForFormVm>() : JsonSerializer.Deserialize<List<OrganisationRoleForFormVm>>(json);
    }
}
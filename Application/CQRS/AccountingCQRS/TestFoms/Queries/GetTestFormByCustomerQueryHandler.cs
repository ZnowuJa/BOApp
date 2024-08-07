using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms;
using AutoMapper;
using Application.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Forms;
using Application.ViewModels.General;
using System.Text.Json;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetTestFormByCustomerQueryHandler : IRequestHandler<GetTestFormByCustomerQuery, IQueryable<TestFormVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetTestFormByCustomerQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<IQueryable<TestFormVm>> Handle(GetTestFormByCustomerQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.TestForms.Where(p => p.KontrahentId == request.Id && (p.Status == "AprobataL1" || p.Status == "AprobataL2")).ToListAsync(cancellationToken);

        var items = new List<TestFormVm>();

        foreach (var item in result)
        {
            items.Add(MapToViewModel(item));
        }
        
        //itemVM;
        //return temp;

        return items.AsQueryable();
    }

    private TestFormVm MapToViewModel(TestForm model)
    {
        var item = new TestFormVm
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
            Level2Approvers = DeserializeRoles(model.Level2Approvers)
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

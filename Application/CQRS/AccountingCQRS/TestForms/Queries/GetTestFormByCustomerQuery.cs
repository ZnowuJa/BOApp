using Application.Forms;
using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using Domain.Forms;
using MediatR;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.TestForms.Queries;
public class GetTestFormByCustomerQuery(string i) : IRequest<IQueryable<TestFormVm>>
{
    public string Id { get; set; } = i;
}
public class GetTestFormByCustomerQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetTestFormByCustomerQuery, IQueryable<TestFormVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

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

    private List<ApprovalVm> DeserializeApprovals(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<ApprovalVm>() : JsonSerializer.Deserialize<List<ApprovalVm>>(json);
    }

    private List<OrganisationRoleForFormVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleForFormVm>() : JsonSerializer.Deserialize<List<OrganisationRoleForFormVm>>(json);
    }
}
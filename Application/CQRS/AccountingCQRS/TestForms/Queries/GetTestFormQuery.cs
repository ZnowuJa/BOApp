using Application.Forms;
using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Application.CQRS.AccountingCQRS.TestForms.Queries;
public class GetTestFormQuery(int i) : IRequest<TestFormVm>
{
    public int Id { get; set; } = i;
}
public class GetTestFormQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetTestFormQuery, TestFormVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public IMapper Mapper { get; }

    public async Task<TestFormVm> Handle(GetTestFormQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine();
        var model = await _appDbContext.TestForms.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        var itemVm = new TestFormVm
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
        };

        return itemVm;
        //return temp;
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
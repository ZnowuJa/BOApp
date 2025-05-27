using Application.Forms;
using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.CQRS.AccountingCQRS.TestForms.Queries;
public class GetAllTestFormsQuery : IRequest<IQueryable<TestFormVm>>
{
}
public class GetAllTestFormsQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllTestFormsQueryHandler> logger) : IRequestHandler<GetAllTestFormsQuery, IQueryable<TestFormVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<TestFormVm>> Handle(GetAllTestFormsQuery request, CancellationToken cancellationToken)
    {
        var dpmntsList = new List<TestFormVm>();

        var dpmnts = await _appDbContext.TestForms.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        foreach (var model in dpmnts)
        {


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
                Level2Approvers = DeserializeRoles(model.Level2Approvers)
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
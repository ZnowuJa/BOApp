using System.Text.Json;
using Application.Forms;
using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms;

using MediatR;

namespace Application.CQRS.AccountingCQRS.TestForms.Commands;
public class CreateTestFormCommandHandler : IRequestHandler<CreateTestFormCommand, TestFormVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateTestFormCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<TestFormVm> Handle(CreateTestFormCommand request, CancellationToken cancellationToken)
    {
        var result = new TestFormVm();
        using var transaction = await _appDbContext.BeginTransactionAsync();
        try
        {
            var item = _mapper.Map<TestForm>(request.Item);
            item.Approvals = SerializeApprovals(request.Item.Approvals);
            item.Level1Approvers = SerializeRole(request.Item.Level1Approvers);
            item.Level2Approvers = SerializeRole(request.Item.Level2Approvers);

            _appDbContext.TestForms.Add(item);
            await _appDbContext.SaveChangesAsync();

            item.Number = $"{item.NumberPrefix}{item.Id.ToString("D10")}";
            item.StatusId = 1;
            _appDbContext.TestForms.Update(item);
            item.Requested = DateTime.Now;
            await _appDbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            request.Item.Number = item.Number;
            request.Item.Status = item.Status;
            request.Item.Requested = item.Requested;

            result = MapToViewModel(item);
            Console.WriteLine();

        }
        catch (Exception ex)
        {

            await transaction.RollbackAsync();
            throw;
        }
        
        return result;
    }
    private string SerializeApprovals(List<Approval> approvals)
    {
        return approvals == null || approvals.Count == 0 ? null : JsonSerializer.Serialize(approvals);
    }

    private string SerializeRole(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? null : JsonSerializer.Serialize(roles);
    }

    private List<Approval> DeserializeApprovals(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<Approval>() : JsonSerializer.Deserialize<List<Approval>>(json);
    }

    private List<OrganisationRoleForFormVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleForFormVm>() : JsonSerializer.Deserialize<List<OrganisationRoleForFormVm>>(json);
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
}

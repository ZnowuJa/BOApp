using System.Text.Json;
using Application.Forms;
using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;

namespace Application.CQRS.AccountingCQRS.TestForms.Commands;
public class UpdateTestFormCommand(TestFormVm item) : IRequest<TestFormVm>
{
    public TestFormVm Item { get; set; } = item;
}

public class UpdateTestFormCommandHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<UpdateTestFormCommand, TestFormVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<TestFormVm> Handle(UpdateTestFormCommand request, CancellationToken cancellationToken)
    {

        using var transaction = await _appDbContext.BeginTransactionAsync();
        try
        {
            var item = await _appDbContext.TestForms.FindAsync(request.Item.Id, cancellationToken);
            if (item != null)
            {
                _mapper.Map(request.Item, item);
                item.Approvals = SerializeApprovals(request.Item.Approvals);
                item.Level1Approvers = SerializeRole(request.Item.Level1Approvers);
                item.Level2Approvers = SerializeRole(request.Item.Level2Approvers);
            }

            _appDbContext.TestForms.Update(item);
            await _appDbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return request.Item;

    }

    private string SerializeApprovals(List<ApprovalVm> approvals)
    {
        return approvals == null || approvals.Count == 0 ? null : JsonSerializer.Serialize(approvals);
    }

    private string SerializeRole(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? null : JsonSerializer.Serialize(roles);
    }

}
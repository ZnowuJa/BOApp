using System.Text.Json;
using Application.Forms.CoC;
using Application.Interfaces;
using Application.ViewModels.CoC;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.Onboarding.Queries;
public class GetOnboardingByEmpIdQuery : IRequest<OnboardingFormVm>
{
    public string Id { get; }
    public GetOnboardingByEmpIdQuery(string id)
    {
        Id = id;
    }


}


public class GetOnboardingByEmpIdQueryHandler : IRequestHandler<GetOnboardingByEmpIdQuery, OnboardingFormVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetOnboardingByEmpIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OnboardingFormVm> Handle(GetOnboardingByEmpIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.OnboardingForms.Where(i => i.EmployeeId == int.Parse(request.Id)).Include(i => i.Group).FirstOrDefaultAsync(cancellationToken);

        var result = _mapper.Map<OnboardingFormVm>(item);
        result.Approvals = DeserializeApprovals(item.Approvals);
        result.Level1Approvers = DeserializeRoles(item.Level1Approvers);
        result.Level2Approvers = DeserializeRoles(item.Level2Approvers);
        result.Instructions = DeserializeInstructions(item.Instructions);
        result.Modified = item.Modified;

        return result;
    }

    private List<ApprovalVm> DeserializeApprovals(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<ApprovalVm>() : JsonSerializer.Deserialize<List<ApprovalVm>>(json);
    }
    private List<OrganisationRoleForFormVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleForFormVm>() : JsonSerializer.Deserialize<List<OrganisationRoleForFormVm>>(json);
    }

    private List<InstructionStatus> DeserializeInstructions(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<InstructionStatus>() : JsonSerializer.Deserialize<List<InstructionStatus>>(json);
    }

}
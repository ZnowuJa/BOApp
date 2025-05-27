using System.Text.Json;
using Application.Forms.CoC;
using Application.Interfaces;
using Application.ViewModels.CoC;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.Onboarding.Queries;
public class GetOnboardingByIdQuery : IRequest<OnboardingFormVm>
{
    public int Id { get; }
    public GetOnboardingByIdQuery(int id)
    {
        Id = id;
    }


}


public class GetOnboardingByIdQueryHandler : IRequestHandler<GetOnboardingByIdQuery, OnboardingFormVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetOnboardingByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OnboardingFormVm> Handle(GetOnboardingByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.OnboardingForms.Where(i => i.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        var result = _mapper.Map<OnboardingFormVm>(item);
        result.Approvals = DeserializeApprovals(item.Approvals);
        result.Level1Approvers = DeserializeRoles(item.Level1Approvers);
        result.Level2Approvers = DeserializeRoles(item.Level2Approvers);
        result.Instructions = DeserializeInstructions(item.Instructions);
        result.Modified = item.Modified;
        var resultVm = new OnboardingFormVm()
        {

        };

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
using System.Text.Json;
using Application.Forms.CoC;
using Application.Interfaces;
using Application.ViewModels.CoC;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.Onboarding.Queries;
public class GetOnboardingsByEmpIdQuery : IRequest<IQueryable<OnboardingFormVm>>
{
    public string Id { get; set; }
    public GetOnboardingsByEmpIdQuery(string i)
    {
        Id = i;
    }
}

public class GetOnboardingsByEmpIdQueryHandler : IRequestHandler<GetOnboardingsByEmpIdQuery, IQueryable<OnboardingFormVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetOnboardingsByEmpIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<IQueryable<OnboardingFormVm>> Handle(GetOnboardingsByEmpIdQuery request, CancellationToken cancellationToken)
    {
        var empId = int.Parse(request.Id);
        var result = await _appDbContext.OnboardingForms.Where(p => p.EmployeeId == empId).ToListAsync(cancellationToken);
        var items = new List<OnboardingFormVm>();

        foreach (var item in result)
        {
            items.Add(MapToViewModel(item));
        }

        return items.AsQueryable();
    }

    private OnboardingFormVm MapToViewModel(OnboardingForm model)
    {
        var item = new OnboardingFormVm
        {
            Id = model.Id,
            Name = model.Title,
            Description = model.Description,
            FolderName = model.FolderName,
            NumberPrefix = model.NumberPrefix,
            Status = model.Status,
            Number = model.Number,
            Note = model.Note,
            EmployeeId = model.EmployeeId,
            EmployeeName = model.EmployeeName,
            Requested = model.Requested,

            Instructions = DeserializeInstructions(model.Instructions),
            Group = model.Group,
            Progress = model.Progress,
            FirstRun = model.FirstRun,

            LVL1_EnovaEmpId = model.LVL1_EnovaEmpId,
            LVL2_EnovaEmpId = model.LVL2_EnovaEmpId,
            LVL1_EmployeeName = model.LVL1_EmployeeName,
            LVL2_EmployeeName = model.LVL2_EmployeeName,
            Approvals = DeserializeApprovals(model.Approvals),
            Level1Approvers = DeserializeRoles(model.Level1Approvers),
            Level2Approvers = DeserializeRoles(model.Level2Approvers),
            Modified = model.Modified
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

    private List<InstructionStatus> DeserializeInstructions(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<InstructionStatus>() : JsonSerializer.Deserialize<List<InstructionStatus>>(json);
    }
}

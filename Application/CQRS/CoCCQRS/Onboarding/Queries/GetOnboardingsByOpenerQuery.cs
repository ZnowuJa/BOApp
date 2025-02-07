using System.Text.Json;
using Application.Forms.CoC;
using Application.Interfaces;
using Application.ViewModels.CoC;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.Onboarding.Queries

{
    public class GetOnboardingsByOpenerQuery : IRequest<IQueryable<OnboardingFormVm>>
    {
        public string EnovaEmpId { get; set; }

        public GetOnboardingsByOpenerQuery(string enovaEmpId)
        {
            EnovaEmpId = enovaEmpId;
        }
    }



    public class GetOnboardingsByOpenerQueryHandler : IRequestHandler<GetOnboardingsByOpenerQuery, IQueryable<OnboardingFormVm>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetOnboardingsByOpenerQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<OnboardingFormVm>> Handle(GetOnboardingsByOpenerQuery request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.OnboardingForms.ToListAsync(cancellationToken);

            var filteredResult = result
                .Where(i => DeserializeRoles(i.Level1Approvers).Any(role => role.EmpId.ToString() == request.EnovaEmpId) && i.StatusId == 1)
                .Select(item => MapToViewModel(item))
                .AsQueryable();

            return filteredResult;

            //var result = await _appDbContext.OnboardingForms
            //    .Where(i => DeserializeRoles(i.Level1Approvers).Any(role => role.EmpId.ToString() == request.EnovaEmpId))
            //    .ToListAsync(cancellationToken);

            //var items = result.Select(item => MapToViewModel(item)).AsQueryable();
            //return items;
        }

        private OnboardingFormVm MapToViewModel(OnboardingForm model)
        {
            return new OnboardingFormVm
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

}

     
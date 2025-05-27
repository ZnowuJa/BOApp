using Application.Interfaces;
using Application.ViewModels.CoC;

using AutoMapper;

using Domain.Entities.CoC;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.GroupCoCs.Commands;
public class CreateGroupCoCCommand : IRequest<int>
{
    public GroupCoCVm GroupCoCVm { get; set; }

    public CreateGroupCoCCommand(GroupCoCVm groupCoCVm)
    {
        GroupCoCVm = groupCoCVm;
    }
};

public class CreateGroupCoCCommandHandler : IRequestHandler<CreateGroupCoCCommand, int>
{
    private readonly IAppDbContext _context;
    private object _mapper;

    public CreateGroupCoCCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateGroupCoCCommand request, CancellationToken cancellationToken)
    {
        var positionIds = request.GroupCoCVm.Positions.Select(p => p.Id).ToList();
        var positions = await _context.Positions
            .Where(p => positionIds.Contains(p.Id))
            .ToListAsync(cancellationToken);
        var group = new GroupCoC()
        {
            GroupName = request.GroupCoCVm.GroupName,
            Positions = positions

        };

        _context.Groups.Add(group);
        await _context.SaveChangesAsync(cancellationToken);

        return group.Id;
    }
}

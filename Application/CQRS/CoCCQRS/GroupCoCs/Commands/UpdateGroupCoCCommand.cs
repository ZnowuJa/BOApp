using Application.Interfaces;
using Application.ViewModels.CoC;

using AutoMapper;
using Domain.Entities.CoC;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.GroupCoCs.Commands;
public class UpdateGroupCoCCommand : IRequest<int> 
{
    public GroupCoCVm Group { get; set; }

    public UpdateGroupCoCCommand(GroupCoCVm groupCoCVm)
    {
        Group = groupCoCVm;
    }

};

public class UpdateGroupCoCCommandHandler : IRequestHandler<UpdateGroupCoCCommand, int>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateGroupCoCCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    

    public async Task<int> Handle(UpdateGroupCoCCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Groups
            .Include(g => g.Positions)
            .FirstOrDefaultAsync(g => g.Id == request.Group.Id, cancellationToken);

        if (entity == null)
        {
            throw new Exception(nameof(GroupCoC));
        }

        // Update positions
        var positionIds = request.Group.Positions.Select(p => p.Id).ToList();
        var positions = await _context.Positions
            .Where(p => positionIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        entity.GroupName = request.Group.GroupName;
        entity.Positions = positions;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}


using Application.Interfaces;
using Application.ViewModels.CoC;
using MediatR;

namespace Application.CQRS.CoCCQRS.Positions.Commands;
public record UpdatePositionCommand(PositionVm Position) : IRequest<int>;

public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, int>
{
    private readonly IAppDbContext _context;

    public UpdatePositionCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _context.Positions.FindAsync(request.Position.Id);

        position.Name = request.Position.Name;
        position.Cat = request.Position.Cat;
        position.GroupCoCId = request.Position.GroupCoCId;

        await _context.SaveChangesAsync(cancellationToken);

        return position.Id; // Returning the updated Position ID
    }
}


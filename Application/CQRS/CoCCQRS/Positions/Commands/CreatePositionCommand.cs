using Application.Interfaces;
using Domain.Entities.CoC;
using MediatR;

namespace Application.CQRS.CoCCQRS.Positions.Commands;
public record CreatePositionCommand(ViewModels.CoC.PositionVm Position) : IRequest<int>;

public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, int>
{
    private readonly IAppDbContext _context;

    public CreatePositionCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = new Position
        {
            Name = request.Position.Name,
            Cat = request.Position.Cat
        };

        _context.Positions.Add(position);
        await _context.SaveChangesAsync(cancellationToken);

        //return 0;
        return position.Id; // Returning the created Position ID
    }
}

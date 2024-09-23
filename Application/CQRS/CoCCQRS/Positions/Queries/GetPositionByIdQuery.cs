using Application.Interfaces;
using Application.ViewModels.CoC;

using MediatR;

namespace Application.CQRS.CoCCQRS.Positions.Queries;
public record GetPositionByIdQuery(int Id) : IRequest<PositionVm>;

public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, PositionVm>
{
    private readonly IAppDbContext _context;

    public GetPositionByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<PositionVm> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
    {
        var position = await _context.Positions.FindAsync(request.Id);

        return new PositionVm(position.Id, position.Name);
    }
}

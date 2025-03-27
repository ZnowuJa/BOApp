using Application.Interfaces;
using Application.ViewModels.CoC;

using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.Positions.Queries;
public class GetAllPositionsQuery : IRequest<IQueryable<PositionVm>>
{ 
};

public class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, IQueryable<PositionVm>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllPositionsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IQueryable<PositionVm>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
    {
        var positions = await _context.Positions
            .Where(p => p.StatusId == 1)
            .ToListAsync(cancellationToken);
        var result = _mapper.Map<List<PositionVm>>(positions);
        foreach (var positionVm in result)
        {
            if (positionVm.GroupCoCId == 0 || positionVm.GroupCoCId == null)
            {
                positionVm.GroupCoCId = 0;
                positionVm.GroupCoC = new GroupCoCVm();
            }
        }
        return result.AsQueryable();
    }
}

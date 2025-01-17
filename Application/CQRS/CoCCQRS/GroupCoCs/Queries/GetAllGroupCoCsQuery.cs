using Application.Interfaces;
using Application.ViewModels.CoC;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.GroupCoCs.Queries;
public class GetAllGroupCoCsQuery : IRequest<IQueryable<GroupCoCVm>>
{
   
};

public class GetAllGroupCoCsQueryHandler : IRequestHandler<GetAllGroupCoCsQuery, IQueryable<GroupCoCVm>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllGroupCoCsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IQueryable<GroupCoCVm>> Handle(GetAllGroupCoCsQuery request, CancellationToken cancellationToken)
    {
        //var groups = await _appDbContext.Groups
        //    .Include(g => g.Positions)
        //    .Select(g => new GroupCoCVm
        //    {
        //        Id = g.Id,
        //        GroupName = g.GroupName,
        //        Positions = g.Positions.Select(p => new PositionVm(p.Id, p.Title)).ToList()
        //    })
        //    .ToListAsync(cancellationToken);

        var groups = await _context.Groups.Include(g => g.Instructions)
                                  .Include(g => g.Positions)
                                  .ToListAsync(cancellationToken);
        var result = _mapper.Map<List<GroupCoCVm>>(groups).AsQueryable();
        //return groups.AsQueryable();
        return result;
    }
}

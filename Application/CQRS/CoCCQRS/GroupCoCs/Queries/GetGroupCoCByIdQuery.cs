using Application.Interfaces;
using Application.ViewModels.CoC;
using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.GroupCoCs.Queries;
public class GetGroupCoCByIdQuery : IRequest<GroupCoCVm>
{
    public int Id { get; set; }

    public GetGroupCoCByIdQuery(int id)
    {
        Id = id;
    }
};

public class GetGroupCoCByIdQueryHandler : IRequestHandler<GetGroupCoCByIdQuery, GroupCoCVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetGroupCoCByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GroupCoCVm> Handle(GetGroupCoCByIdQuery request, CancellationToken cancellationToken)
    {
        var group = await _context.Groups
            .Include(g => g.Positions)
            .Include(i => i.Instructions)
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
        var result =  _mapper.Map<GroupCoCVm>(group);
        //if (Group == null)
        //{
        //    throw new NotFoundException(nameof(GroupCoC), request.Id);
        //}
        //var poss = Group.Positions
        //    .Select(pos => new Position
        //    {
        //        Title = pos.Title,
        //        Cat = pos.Cat
        //    })
        //    .ToList();
        //var poss = _mapper.Map<List<PositionVm>>(group.Positions);

        //return new GroupCoCVm
        //{
        //    Id = group.Id,
        //    GroupName = group.GroupName,
        //    Positions = poss
        //    //Positions = Group.Positions.Select(p => new PositionVm(p.Id, p.Title)).ToList()
        //};
        return result;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.ViewModels.CoC;

using AutoMapper;

using Domain.Entities.CoC;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.InstructionCoCs.Queries;
public class GetInstructionCoCByIdQuery : IRequest<InstructionCoCVm>
{
    public int Id { get; set; }

    public GetInstructionCoCByIdQuery(int id)
    {
        Id = id;
    }
};

public class GetInstructionCoCByIdQueryHandler : IRequestHandler<GetInstructionCoCByIdQuery, InstructionCoCVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetInstructionCoCByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<InstructionCoCVm> Handle(GetInstructionCoCByIdQuery request, CancellationToken cancellationToken)
    {
        var instruction = await _context.Instructions
            .Include(i => i.Groups)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        //if (instruction == null)
        //{
        //    throw new NotFoundException(nameof(InstructionCoC), request.Id);
        //}

        //return new InstructionCoCVm
        //{
        //    Id = instruction.Id,
        //    Title = instruction.Title,
        //    Number = instruction.Number,
        //    Description = instruction.Description,
        //    Published = instruction.Published,
        //    Groups = instruction.Groups.Select(g => new GroupCoCVm
        //    {
        //        Id = g.Id,
        //        GroupName = g.GroupName,
        //        Positions = g.Positions.Select(p => new PositionVm(p.Id, p.Name)).ToList()
        //    }).ToList(),
        //    Link = instruction.Link
        //};

        var result = _mapper.Map<InstructionCoCVm>(instruction);
        return result;

    }
}


using Application.Interfaces;
using Application.ViewModels.CoC;
using AutoMapper;

using Domain.Entities.CoC;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.InstructionCoCs.Commands;
public class CreateInstructionCoCCommand : IRequest<int> 
{ 
    public InstructionCoCVm Instruction {  get; set; }

    public CreateInstructionCoCCommand(InstructionCoCVm instruction)
    {
        Instruction = instruction;
    }
};

public class CreateInstructionCoCCommandHandler : IRequestHandler<CreateInstructionCoCCommand, int>
{
    private readonly IAppDbContext _context;
    public IMapper _mapper { get; }

    public CreateInstructionCoCCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    

    public async Task<int> Handle(CreateInstructionCoCCommand request, CancellationToken cancellationToken)
    {
        var groupIds = request.Instruction.Groups.Select(p => p.Id).ToList();
        var groups = await _context.Groups.Where(p => groupIds.Contains(p.Id)).ToListAsync(cancellationToken);

        var instruction = new InstructionCoC
        {
            Title = request.Instruction.Title,
            Number = request.Instruction.Number,
            Description = request.Instruction.Description,
            Published = request.Instruction.Published,
            //Groups = request.Instruction.Groups.Select(g => new GroupCoC { Id = g.Id }).ToList(),
            Groups =groups,
            Link = request.Instruction.Link,
            Priority = (int)request.Instruction.Priority,
            Colour = request.Instruction.Colour
        };

        _context.Instructions.Add(instruction);
        await _context.SaveChangesAsync(cancellationToken);

        return instruction.Id;
    }
}

using Application.Interfaces;
using Application.ViewModels.CoC;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.InstructionCoCs.Commands;
public class UpdateInstructionCoCCommand : IRequest<int>
{
    public InstructionCoCVm Instruction { get; set; }

    public UpdateInstructionCoCCommand(InstructionCoCVm instructionCoCVm)
    {
        Instruction = instructionCoCVm;
    }
};



public class UpdateInstructionCoCCommandHandler : IRequestHandler<UpdateInstructionCoCCommand, int>
{
    private readonly IAppDbContext _context;

    public UpdateInstructionCoCCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateInstructionCoCCommand request, CancellationToken cancellationToken)
    {
        var instruction = await _context.Instructions.FindAsync(request.Instruction.Id);
        var groupIds = request.Instruction.Groups.Select(p => p.Id).ToList();
        var groups = await _context.Groups.Where(p => groupIds.Contains(p.Id)).ToListAsync(cancellationToken);

        instruction.Title = request.Instruction.Title;
        instruction.Number = request.Instruction.Number;
        instruction.Description = request.Instruction.Description;
        instruction.Published = request.Instruction.Published;
        instruction.Groups = groups;
        instruction.Link = request.Instruction.Link;
        instruction.Priority = (int)request.Instruction.Priority;
        instruction.Colour = request.Instruction.Colour;

        await _context.SaveChangesAsync(cancellationToken);

        return instruction.Id;
    }
}


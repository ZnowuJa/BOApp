using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Domain.Entities.CoC;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.InstructionCoCs.Commands;
public class DeleteInstructionCoCCommand : IRequest<int>
{
    public int Id { get; set; }

    public DeleteInstructionCoCCommand(int id)
    {
        Id = id;
    }
}

public class DeleteInstructionCoCCommandHandler : IRequestHandler<DeleteInstructionCoCCommand, int>
{
    private readonly IAppDbContext _context;

    public DeleteInstructionCoCCommandHandler(IAppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<int> Handle(DeleteInstructionCoCCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Instructions.FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        //if (entity == null)
        //    throw new NotFoundException(nameof(InstructionCoC), request.Id);

        _context.Instructions.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return request.Id;
    }
}

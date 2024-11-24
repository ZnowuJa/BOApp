using Application.Interfaces;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Scrappings.Commands;
public class DeleteITScrappingFormCommand : IRequest<int>
{
    public int Id { get; set; }

    public DeleteITScrappingFormCommand(int id)
    {
        Id = id;
    }
}

public class DeleteITScrappingFormCommandHandler : IRequestHandler<DeleteITScrappingFormCommand, int>
{
    private readonly IAppDbContext _context;

    public DeleteITScrappingFormCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteITScrappingFormCommand command, CancellationToken cancellationToken)
    {
        var form = await _context.ITScrappingForms.FindAsync(command.Id);
        if (form != null)
        {
            _context.ITScrappingForms.Remove(form);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return form.Id;
    }
}

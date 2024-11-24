using Application.Interfaces;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Sales.Commands;
public class DeleteITSaleFormCommand : IRequest<int>
{
    public int Id { get; set; }

    public DeleteITSaleFormCommand(int id)
    {
        Id = id;
    }
}

public class DeleteITSaleFormCommandHandler : IRequestHandler<DeleteITSaleFormCommand, int>
{
    private readonly IAppDbContext _context;

    public DeleteITSaleFormCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteITSaleFormCommand command, CancellationToken cancellationToken)
    {
        var form = await _context.ITSaleForms.FindAsync(command.Id);
        if (form != null)
        {
            _context.ITSaleForms.Remove(form);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return form.Id;
    }
}

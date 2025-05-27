using Application.Forms.IT;
using Application.Interfaces;
using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Sales.Commands;
public class UpdateITSaleFormCommand : IRequest<ITSaleFormVm>
{
    public ITSaleFormVm Form { get; set; }

    public UpdateITSaleFormCommand(ITSaleFormVm form)
    {
        Form = form;
    }
}

public class UpdateITSaleFormCommandHandler : IRequestHandler<UpdateITSaleFormCommand, ITSaleFormVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateITSaleFormCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ITSaleFormVm> Handle(UpdateITSaleFormCommand command, CancellationToken cancellationToken)
    {
        //var form = _mapper.Map<ITSaleForm>(command.Form);

        var existingSaleForm = await _context.ITSaleForms.FirstOrDefaultAsync(x => x.Id == command.Form.Id);

        _mapper.Map(command.Form, existingSaleForm);

        _context.ITSaleForms.Update(existingSaleForm);
        await _context.SaveChangesAsync(cancellationToken);
        

        return command.Form;
    }
}

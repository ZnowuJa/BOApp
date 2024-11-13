using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Forms.IT;
using Application.Interfaces;
using AutoMapper;
using Domain.Forms.ITForms;
using MediatR;

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
        var form = _mapper.Map<ITSaleForm>(command.Form);
        _context.ITSaleForms.Update(form);
        await _context.SaveChangesAsync(cancellationToken);
        command.Form.Id = form.Id;

        return command.Form;
    }
}

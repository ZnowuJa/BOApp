using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms;
using Application.Interfaces;
using AutoMapper;
using Domain.Forms.ITForms;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Sales.Commands;
public class CreateITSaleFormCommand : IRequest<ITSaleFormVm>
{
    public ITSaleFormVm Form { get; set; }

    public CreateITSaleFormCommand(ITSaleFormVm form)
    {
        Form = form;
    }
}

public class CreateITSaleFormCommandHandler : IRequestHandler<CreateITSaleFormCommand, ITSaleFormVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateITSaleFormCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ITSaleFormVm> Handle(CreateITSaleFormCommand command, CancellationToken cancellationToken)
    {
        var form = _mapper.Map<ITSaleForm>(command.Form);
        _context.ITSaleForms.Add(form);
        await _context.SaveChangesAsync(cancellationToken);
        command.Form.Id = form.Id;
        command.Form.Number = $"{command.Form.NumberPrefix}{command.Form.Id.ToString("D6")}";
        form = _mapper.Map<ITSaleForm>(command.Form);
        _context.ITSaleForms.Update(form);
        await _context.SaveChangesAsync();

        return command.Form;
    }

}

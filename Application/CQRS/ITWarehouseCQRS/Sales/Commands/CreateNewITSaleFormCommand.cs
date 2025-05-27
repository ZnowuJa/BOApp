using Application.Forms.IT;
using Application.Interfaces;
using AutoMapper;
using Domain.Forms.ITForms;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Sales.Commands;
public class CreateNewITSaleFormCommand : IRequest<ITSaleFormVm>
{
    
}

public class CreateNewITSaleFormCommandHandler : IRequestHandler<CreateNewITSaleFormCommand, ITSaleFormVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateNewITSaleFormCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ITSaleFormVm> Handle(CreateNewITSaleFormCommand command, CancellationToken cancellationToken)
    {
        var form = new ITSaleForm();
        _context.ITSaleForms.Add(form);
        await _context.SaveChangesAsync(cancellationToken);
        

        var newForm  = await _context.ITSaleForms.Where(f => f.Id == form.Id).FirstAsync();
        var result = _mapper.Map<ITSaleFormVm>(newForm);

        return result;
    }

}

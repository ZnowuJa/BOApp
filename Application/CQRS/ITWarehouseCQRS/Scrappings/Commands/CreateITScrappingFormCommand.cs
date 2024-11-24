using Application.Forms.IT;
using Application.Interfaces;
using AutoMapper;
using Domain.Forms.ITForms;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Scrappings.Commands;
public class CreateITScrappingFormCommand : IRequest<ITScrappingFormVm>
{
    public ITScrappingFormVm Form { get; set; }

    public CreateITScrappingFormCommand(ITScrappingFormVm form)
    {
        Form = form;
    }
}

public class CreateITScrappingFormCommandHandler : IRequestHandler<CreateITScrappingFormCommand, ITScrappingFormVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateITScrappingFormCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ITScrappingFormVm> Handle(CreateITScrappingFormCommand command, CancellationToken cancellationToken)
    {
        var form = _mapper.Map<ITScrappingForm>(command.Form);
        _context.ITScrappingForms.Add(form);
        await _context.SaveChangesAsync(cancellationToken);
        command.Form.Id = form.Id;

        return command.Form;
    }

}

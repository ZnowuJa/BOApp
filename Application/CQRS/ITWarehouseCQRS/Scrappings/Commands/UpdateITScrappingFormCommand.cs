using Application.Forms.IT;
using Application.Interfaces;
using AutoMapper;
using Domain.Forms.ITForms;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Scrappings.Commands;
public class UpdateITScrappingFormCommand : IRequest<ITScrappingFormVm>
{
    public ITScrappingFormVm Form { get; set; }

    public UpdateITScrappingFormCommand(ITScrappingFormVm form)
    {
        Form = form;
    }
}

public class UpdateITScrappingFormCommandHandler : IRequestHandler<UpdateITScrappingFormCommand,ITScrappingFormVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateITScrappingFormCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ITScrappingFormVm> Handle(UpdateITScrappingFormCommand command, CancellationToken cancellationToken)
    {
        var form = _mapper.Map<ITScrappingForm>(command.Form);
        _context.ITScrappingForms.Update(form);
        await _context.SaveChangesAsync(cancellationToken);
        command.Form.Id = form.Id;

        return command.Form;
    }
}

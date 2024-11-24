using Application.Forms.IT;
using Application.Interfaces;
using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Scrappings.Queries;
public class GetAllITScrappingFormsQuery : IRequest<List<ITScrappingFormVm>>
{
}

public class GetAllITScrappingFormsQueryHandler : IRequestHandler<GetAllITScrappingFormsQuery, List<ITScrappingFormVm>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllITScrappingFormsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ITScrappingFormVm>> Handle(GetAllITScrappingFormsQuery query, CancellationToken cancellationToken)
    {
        var forms = await _context.ITScrappingForms
            .Include(f => f.Assets)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ITScrappingFormVm>>(forms);
    }
}

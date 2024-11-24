using Application.Forms.IT;
using Application.Interfaces;
using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Scrappings.Queries;
public class GetITScrappingByIdQuery : IRequest<ITScrappingFormVm>
{
    public int Id { get; set; }

    public GetITScrappingByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetITScrappingByIdQueryHandler : IRequestHandler<GetITScrappingByIdQuery, ITScrappingFormVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetITScrappingByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ITScrappingFormVm> Handle(GetITScrappingByIdQuery query, CancellationToken cancellationToken)
    {
        var form = await _context.ITScrappingForms
            
            
            .Include(f => f.Assets)
            .FirstOrDefaultAsync(f => f.Id == query.Id, cancellationToken);

        return _mapper.Map<ITScrappingFormVm>(form);
    }
}

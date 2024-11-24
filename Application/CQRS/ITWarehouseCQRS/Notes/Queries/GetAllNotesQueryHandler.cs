using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.ITWarehouseCQRS.Notes.Queries;
public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, IQueryable<NoteVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllNotesQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllNotesQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<NoteVm>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        var curs = await _appDbContext.Notes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var curslist = _mapper.Map<List<NoteVm>>(curs);

        return curslist.AsQueryable();
    }

}

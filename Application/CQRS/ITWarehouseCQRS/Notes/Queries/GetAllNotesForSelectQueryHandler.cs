using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.ITWarehouseCQRS.Notes.Queries;
public class GetAllNotesForSelectQueryHandler : IRequestHandler<GetAllNotesForSelectQuery, IQueryable<NoteVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllNotesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllNotesForSelectQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<NoteVm>> Handle(GetAllNotesForSelectQuery request, CancellationToken cancellationToken)
    {
        List<Note> itemsSelected = new();
        Note itemFirst = new Note() { Id = 0, Text = "Select EmployeType ..." };
        itemsSelected.Add(itemFirst);
        var itemsFromDb = await _appDbContext.Notes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        itemsSelected.AddRange(itemsFromDb);
        var itemsList = _mapper.Map<List<NoteVm>>(itemsSelected);

        return itemsList.AsQueryable();
    }
}

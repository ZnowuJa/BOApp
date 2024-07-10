using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Notes.Queries;
public class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, NoteVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetNoteQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<NoteVm> Handle(GetNoteQuery request, CancellationToken cancellationToken)
    {
        var note = await _appDbContext.Notes.Where(p => p.Id == request.NoteId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var noteVM = _mapper.Map<NoteVm>(note);
        return noteVM;
    }
}

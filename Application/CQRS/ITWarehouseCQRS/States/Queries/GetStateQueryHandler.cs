using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.States.Queries;
public class GetStateQueryHandler : IRequestHandler<GetStateQuery, StateVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetStateQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<StateVm> Handle(GetStateQuery request, CancellationToken cancellationToken)
    {
        var note = await _appDbContext.States.Where(p => p.Id == request.StateId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var noteVM = _mapper.Map<StateVm>(note);
        return noteVM;
    }
}

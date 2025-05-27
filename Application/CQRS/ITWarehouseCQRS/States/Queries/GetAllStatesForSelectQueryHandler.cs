using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.ITWarehouseCQRS.States.Queries;
public class GetAllStatesForSelectQueryHandler : IRequestHandler<GetAllStatesForSelectQuery, IQueryable<StateVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllStatesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllStatesForSelectQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<StateVm>> Handle(GetAllStatesForSelectQuery request, CancellationToken cancellationToken)
    {
        List<State> itemsSelected = new();
        State itemFirst = new State() { Id = 0, Name = "Select..." };
        itemsSelected.Add(itemFirst);
        var itemsFromDb = await _appDbContext.States.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        itemsSelected.AddRange(itemsFromDb);
        var itemsList = _mapper.Map<List<StateVm>>(itemsSelected);

        return itemsList.AsQueryable();
    }
}

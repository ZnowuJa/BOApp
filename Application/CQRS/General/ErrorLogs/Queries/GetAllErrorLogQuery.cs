using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.ErrorLogs.Queries;
public class GetAllErrorLogsQuery : IRequest<List<ErrorLogVm>>
{
}

public class GetAllErrorLogsQueryHandler : IRequestHandler<GetAllErrorLogsQuery, List<ErrorLogVm>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetAllErrorLogsQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<ErrorLogVm>> Handle(GetAllErrorLogsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ErrorLogs.ToListAsync();

        return _mapper.Map<List<ErrorLogVm>>(entities);
    }
}
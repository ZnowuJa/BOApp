using Application.Interfaces;
using Application.ViewModels.General;
using Domain.Entities.Common;
using AutoMapper;

using MediatR;

namespace Application.CQRS.General.ErrorLogs.Commands;
public class CreateErrorLogCommand : IRequest<int>
{
    public ErrorLogVm errorLog { get; set; }

    public CreateErrorLogCommand(ErrorLogVm ErrorLog)
    {
        errorLog = ErrorLog;
    }
}

public class CreateErrorLogCommandHandler : IRequestHandler<CreateErrorLogCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public CreateErrorLogCommandHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<int> Handle(CreateErrorLogCommand request, CancellationToken cancellationToken)
    {

        //var entity = _mapper.Map<Domain.Entities.Common.ErrorLog>(request.errorLog);
        ErrorLog entity = new ErrorLog
        {
            TimeStamp = request.errorLog.TimeStamp,
            User = request.errorLog.User,
            PageUrl = request.errorLog.PageUrl,
            Message = request.errorLog.Message,
            StackTrace = request.errorLog.StackTrace
        };
        _context.ErrorLogs.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

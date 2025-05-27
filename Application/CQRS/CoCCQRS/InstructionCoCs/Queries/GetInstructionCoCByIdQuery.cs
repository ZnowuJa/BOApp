using Application.Interfaces;
using Application.ViewModels.CoC;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.InstructionCoCs.Queries;
public class GetInstructionCoCByIdQuery : IRequest<InstructionCoCVm>
{
    public int Id { get; set; }

    public GetInstructionCoCByIdQuery(int id)
    {
        Id = id;
    }
};

public class GetInstructionCoCByIdQueryHandler : IRequestHandler<GetInstructionCoCByIdQuery, InstructionCoCVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetInstructionCoCByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<InstructionCoCVm> Handle(GetInstructionCoCByIdQuery request, CancellationToken cancellationToken)
    {
        var instruction = await _context.Instructions
            .Include(i => i.Groups)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        var result = _mapper.Map<InstructionCoCVm>(instruction);

        return result;
    }
}


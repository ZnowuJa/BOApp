using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.States.Commands;
public class CreateStateCommandHandler : IRequestHandler<CreateStateCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public CreateStateCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(CreateStateCommand request, CancellationToken cancellationToken)
    {
        State note = new()
        {
            Name = request.Name,
            Description = request.Description,
            StatusId = 1
        };
        _appDbContext.States.Add(note);
        await _appDbContext.SaveChangesAsync();

        return note.Id;
    }
}


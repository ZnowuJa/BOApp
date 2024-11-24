using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Commands;
public class CreateEmployeeTypeCommandHandler : IRequestHandler<CreateEmployeeTypeCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateEmployeeTypeCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateEmployeeTypeCommand request, CancellationToken cancellationToken)
    {
        EmployeeType employeetype = new()
        {
            Name = request.Name,
            StatusId = 1
        };
        _context.EmployeeTypes.Add(employeetype);
        await _context.SaveChangesAsync();

        return employeetype.Id;
    }
}

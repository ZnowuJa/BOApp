using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.CompanyTypes.Commands;
public class CreateCompanyTypeCommand(string name) : IRequest<int>
{
    public string Name { get; set; } = name;
}
public class CreateCompanyTypeCommandHandler(IAppDbContext context) : IRequestHandler<CreateCompanyTypeCommand, int>
{
    private readonly IAppDbContext _context = context;

    public async Task<int> Handle(CreateCompanyTypeCommand request, CancellationToken cancellationToken)
    {
        CompanyType companytype = new()
        {
            Name = request.Name,
            StatusId = 1
        };
        _context.CompanyTypes.Add(companytype);
        await _context.SaveChangesAsync();

        return companytype.Id;
    }
}
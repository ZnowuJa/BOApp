using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.ITWarehouseCQRS.CompanyTypes.Commands;
public class CreateCompanyTypeCommandHandler : IRequestHandler<CreateCompanyTypeCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateCompanyTypeCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

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


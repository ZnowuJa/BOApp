using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.CompanyTypes.Commands;
public class UpdateCompanyTypeCommand(int id, string name) : IRequest<int>
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
}
public class UpdateCompanyTypeCommandHandler(IAppDbContext context) : IRequestHandler<UpdateCompanyTypeCommand, int>
{
    private readonly IAppDbContext _context = context;

    public async Task<int> Handle(UpdateCompanyTypeCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var companytype = await _context.CompanyTypes.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        companytype.Name = request.Name;
        await _context.SaveChangesAsync();
        return companytype.Id;

    }
}
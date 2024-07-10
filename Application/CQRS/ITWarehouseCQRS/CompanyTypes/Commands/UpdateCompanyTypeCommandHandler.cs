using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CompanyTypes.Commands;
public class UpdateCompanyTypeCommandHandler : IRequestHandler<UpdateCompanyTypeCommand, int>
{
    private readonly IAppDbContext _context;

    public UpdateCompanyTypeCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateCompanyTypeCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var companytype = await _context.CompanyTypes.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        companytype.Name = request.Name;
        await _context.SaveChangesAsync();
        return companytype.Id;

    }
}
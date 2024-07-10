using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CompanyTypes.Commands;
public class DeleteCompanyTypeCommandHandler : IRequestHandler<DeleteCompanyTypeCommand, int>
{
    private readonly IAppDbContext _context;

    public DeleteCompanyTypeCommandHandler(IAppDbContext context)
    {
        _context = context;
        Console.WriteLine("Byłem tu!");
    }

    public async Task<int> Handle(DeleteCompanyTypeCommand request, CancellationToken cancellationToken)
    {

        var ct = await _context.CompanyTypes.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _context.CompanyTypes.Remove(ct);
        await _context.SaveChangesAsync();
        //_logger.LogInformation("DeleteCTHandler : " + ct.Id);
        return ct.Id;
    }
}


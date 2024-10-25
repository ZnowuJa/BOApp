using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.Countries.Commands
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        public DeleteCountryCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.Countries
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new KeyNotFoundException($"Countries with Id {request.Id} not found.");

            _appDbContext.Countries.Remove(result);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return result.Id;
        }
    }
}

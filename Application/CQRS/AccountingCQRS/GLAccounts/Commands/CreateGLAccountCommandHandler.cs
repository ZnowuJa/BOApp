using Application.Interfaces;
using Domain.Entities.Accounting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Commands
{
    public class CreateGLAccountCommandHandler : IRequestHandler<CreateGLAccountCommand, int>
    {
        private readonly IAppDbContext _appDbContext;

        public CreateGLAccountCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Handle(CreateGLAccountCommand request, CancellationToken cancellationToken)
        {
            GLAccount glAccount = new()
            {
                AccountNumber = request.AccountNumber,
                Description = request.Description,
                StatusId = 1
            };

            _appDbContext.GLAccounts.Add(glAccount);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return glAccount.Id;
        }
    }
}

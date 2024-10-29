using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using Domain.Entities.Accounting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Commands
{
    namespace Application.CQRS.AccountingCQRS.GLAccounts.Commands
    {
        public class CreateGLAccountCommand : IRequest<int>
        {
            public GLAccountVm GLAccount { get; set; }

            public CreateGLAccountCommand(GLAccountVm glAccount)
            {
                GLAccount = glAccount;
            }
        }

        public class CreateGLAccountCommandHandler : IRequestHandler<CreateGLAccountCommand, int>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public CreateGLAccountCommandHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateGLAccountCommand request, CancellationToken cancellationToken)
            {
                var glAccount = _mapper.Map<GLAccount>(request.GLAccount);
                glAccount.StatusId = 1;

                _context.GLAccounts.Add(glAccount);
                await _context.SaveChangesAsync(cancellationToken);

                return glAccount.Id;
            }
        }
    }

}

using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using Domain.Entities.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Commands
{   
    public class CreateGLAccountCommand(GLAccountVm glAccount) : IRequest<int>
    {
        public GLAccountVm GLAccount { get; set; } = glAccount;
    }

    public class CreateGLAccountCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<CreateGLAccountCommand, int>
    {
        private readonly IAppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

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

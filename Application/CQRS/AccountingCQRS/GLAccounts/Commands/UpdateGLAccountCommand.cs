using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Commands
{
    public class UpdateGLAccountCommand(GLAccountVm glAccount) : IRequest<int>
    {
        public GLAccountVm GLAccount { get; set; } = glAccount;
    }

    public class UpdateGLAccountCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<UpdateGLAccountCommand, int>
    {
        private readonly IAppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(UpdateGLAccountCommand request, CancellationToken cancellationToken)
        {
            var glAccount = await _context.GLAccounts.FirstOrDefaultAsync(g => g.Id == request.GLAccount.Id, cancellationToken);

            _mapper.Map(request.GLAccount, glAccount);
            await _context.SaveChangesAsync(cancellationToken);
            return glAccount.Id;
        }
    }
}

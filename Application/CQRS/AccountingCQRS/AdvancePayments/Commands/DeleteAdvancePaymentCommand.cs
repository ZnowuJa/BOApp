using Application.Forms.Accounting;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.AccountingCQRS.AdvancePayments.Commands
{
    public class DeleteAdvancePaymentCommand (AdvancePaymentFormVm item) : IRequest<int>
    {
        public AdvancePaymentFormVm Item { get; set; } = item;
    }

    public class DeleteAdvancePaymentCommandHandler(IAppDbContext context) : IRequestHandler<DeleteAdvancePaymentCommand, int>
    {
        private readonly IAppDbContext _appDbContext = context;

        public async Task<int> Handle(DeleteAdvancePaymentCommand request, CancellationToken cancellationToken)
        {
            var item = _appDbContext.AdvancePayments.Where(b => b.Id == request.Item.Id).First() ?? throw new KeyNotFoundException($"Adavcne Payment Form with Id {request.Item.Number} not found.");
            _appDbContext.AdvancePayments.Remove(item);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
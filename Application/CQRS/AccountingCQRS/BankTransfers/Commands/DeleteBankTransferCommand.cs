using Application.Forms.Accounting;
using Application.Interfaces;

using AutoMapper;

using Domain.Forms.Accounting;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ExternalConnectors;

namespace Application.CQRS.AccountingCQRS.BankTransfers.Commands
{
    public class DeleteBankTransferCommand(BankTransferFormVm item) : IRequest<int>
    {
        public BankTransferFormVm Item { get; set; } = item;

    }

    public class DeleteBankTransferCommanddHandler(IAppDbContext context) : IRequestHandler<DeleteBankTransferCommand, int>
    {
        private readonly IAppDbContext _appDbContext = context;

        public async Task<int> Handle(DeleteBankTransferCommand request, CancellationToken cancellationToken)
        {
            var item = _appDbContext.BankTransfers.Where(b => b.Id == request.Item.Id).First() ?? throw new KeyNotFoundException($"Business Travel Form with Id {request.Item.Number} not found.");
            ;
            _appDbContext.BankTransfers.Remove(item);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return item.Id;
        }


    }
}
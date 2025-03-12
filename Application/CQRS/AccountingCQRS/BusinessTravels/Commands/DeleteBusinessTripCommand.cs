
using Application.Forms.Accounting;
using Application.Interfaces;

using AutoMapper;

using Domain.Forms.Accounting;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ExternalConnectors;

namespace Application.CQRS.AccountingCQRS.BusinessTravels.Commands
{
    public class DeleteBusinessTripCommand(BusinessTravelFormVm item) : IRequest<int>
    {
        public BusinessTravelFormVm Item { get; set; } = item;

    }

    public class DeleteBusinessTripCommanddHandler(IAppDbContext context) : IRequestHandler<DeleteBusinessTripCommand, int>
    {
        private readonly IAppDbContext _appDbContext = context;

        public async Task<int> Handle(DeleteBusinessTripCommand request, CancellationToken cancellationToken)
        {
            var item = _appDbContext.BusinessTravels.Where(b => b.Id == request.Item.Id).First() ?? throw new KeyNotFoundException($"Business Travel Form with Id {request.Item.Number} not found.");
            ;
            _appDbContext.BusinessTravels.Remove(item);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return item.Id;
        }


    }
}


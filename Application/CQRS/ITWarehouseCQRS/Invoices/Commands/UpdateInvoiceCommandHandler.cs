using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public UpdateInvoiceCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Invoices.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

        result.Number = request.Number;
        result.CompanyId = request.CompanyVmId;
        result.Date = request.Date;
        result.TotalNet = request.TotalNet;
        result.StatusId = 1;
        result.CurrencyId = request.CurrencyVmId;


        _appDbContext.Invoices.Update(result);
        await _appDbContext.SaveChangesAsync();
        return result.Id;
    }
}

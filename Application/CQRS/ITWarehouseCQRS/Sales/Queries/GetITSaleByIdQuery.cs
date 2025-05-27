using Application.Forms.IT;
using Application.Interfaces;
using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Sales.Queries;
public class GetITSaleByIdQuery : IRequest<ITSaleFormVm>
{
    public int Id { get; set; }

    public GetITSaleByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetITSaleByIdQueryHandler : IRequestHandler<GetITSaleByIdQuery, ITSaleFormVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetITSaleByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ITSaleFormVm> Handle(GetITSaleByIdQuery query, CancellationToken cancellationToken)
    {
        var assets = _context.Assets.Where(a => a.StatusId == 1);
        var form = await _context.ITSaleForms
            
            //.Include(f => f.Company)
            //.Include(f => f.Assets)
            .FirstOrDefaultAsync(f => f.Id == query.Id, cancellationToken);

        if (form == null)
        {
            // Handle not found case
            return null;
        }

        var saleFormVm = _mapper.Map<ITSaleFormVm>(form);

        // Map AssetIds to AssetDTOs
        //var assetDtos = await _context.Assets
        //    .Where(a => form.AssetIds.Contains(a.Id))
        //    .Select(a => _mapper.Map<AssetDTO>(a))
        //    .ToListAsync(cancellationToken);

        //saleFormVm.Assets = assetDtos;

        return saleFormVm;
    }
}

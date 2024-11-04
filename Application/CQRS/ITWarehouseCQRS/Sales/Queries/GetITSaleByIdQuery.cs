using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms;
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
        var form = await _context.ITSaleForms
            .Include(f => f.FormFiles)
            .Include(f => f.Company)
            .Include(f => f.Assets)
            .FirstOrDefaultAsync(f => f.Id == query.Id, cancellationToken);

        return _mapper.Map<ITSaleFormVm>(form);
    }
}

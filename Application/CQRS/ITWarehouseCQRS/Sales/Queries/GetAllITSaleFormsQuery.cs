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
public class GetAllITSaleFormsQuery : IRequest<List<ITSaleFormVm>>
{
}

public class GetAllITSaleFormsQueryHandler : IRequestHandler<GetAllITSaleFormsQuery, List<ITSaleFormVm>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllITSaleFormsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ITSaleFormVm>> Handle(GetAllITSaleFormsQuery query, CancellationToken cancellationToken)
    {
        var forms = await _context.ITSaleForms
            .Include(f => f.FormFiles)
            //.Include(f => f.Company)
            //.Include(f => f.Assets)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ITSaleFormVm>>(forms);
    }
}

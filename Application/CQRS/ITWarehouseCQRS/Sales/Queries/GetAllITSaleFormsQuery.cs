using Application.Forms.IT;
using Application.Interfaces;
using Application.ViewModels.General;
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
        var formVms = new List<ITSaleFormVm>();
        var forms = await _context.ITSaleForms.AsNoTracking()
            .Where(s => s.StatusId == 1)
            
            //.Include(f => f.Company)
            //.Include(f => f.Assets)
            .ToListAsync(cancellationToken);
        foreach (var form in forms)
        { 
            var res = _mapper.Map<ITSaleFormVm>(form);
            var files = await _context.FormFiles.Where(f => f.FormId == form.Id && f.FormClassName == "ITSaleFormVm").ToListAsync(cancellationToken);
            res.FormFiles.RemoveAll(f => !files.Any(file => file.Id == f.Id));
            //res.Assets.Clear();
            //res.AssetIds.Clear();
            //res.AssetIds = JsonSerializer.Deserialize<List<int>>(form.AssetIds);

            formVms.Add(res);
        }
        //var formVms = _mapper.Map<List<ITSaleFormVm>>(forms);
        return formVms;
    }
}

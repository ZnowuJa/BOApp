using Application.DTOs;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Assets.Queries;
public class GetAssetDTObyIdQuery : IRequest<AssetDTO>
{
    public int assetId { get; set; }
    public GetAssetDTObyIdQuery(int Id)
    {
        assetId = Id;
    }

}

public class GetAssetDTObyIdQueryHandler : IRequestHandler<GetAssetDTObyIdQuery, AssetDTO>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    //private readonly Stopwatch _stopwatch;
    public GetAssetDTObyIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        //_stopwatch = new Stopwatch();
    }
    public async Task<AssetDTO> Handle(GetAssetDTObyIdQuery request, CancellationToken cancellationToken)
    {
        //var listItems = new List<AssetDTO>();

        var parts = await _appDbContext.Parts.ToListAsync(cancellationToken);
        var invoices = await _appDbContext.Invoices.ToListAsync(cancellationToken);
        var states = await _appDbContext.States.ToListAsync(cancellationToken);
        var employees = await _appDbContext.Employees.ToListAsync(cancellationToken);
        var departments = await _appDbContext.Departments.ToListAsync(cancellationToken);
        var warehouses = await _appDbContext.Warehouses.ToListAsync(cancellationToken);
        var currencies = await _appDbContext.Currencies.ToListAsync(cancellationToken);

        var result = await _appDbContext.Assets.Where(p => p.StatusId == 1 && p.Id == request.assetId).FirstOrDefaultAsync(cancellationToken);

            var part = parts.FirstOrDefault(p => p.Id == result.PartId);
            var invoice = invoices.FirstOrDefault(p => p.Id == result.InvoiceId);
            var state = states.FirstOrDefault(p => p.Id == result.StateId);
            var warehouse = warehouses.FirstOrDefault(p => p.Id == result.WarehouseId);
            var currency = currencies.FirstOrDefault(p => p.Id == result.CurrencyId);
            IAssigneeVm vm = null;
            if (result.AssigneeType == "DepartmentVm")
            {
                var dept = departments.FirstOrDefault(p => p.Id == result.AssigneeId);
                vm = _mapper.Map<DepartmentVm>(dept);

            }
            else if (result.AssigneeType == "EmployeeVm")
            {
                var empl = employees.FirstOrDefault(p => p.EnovaEmpId == result.AssigneeId);
                vm = _mapper.Map<EmployeeVm>(empl);

            }
            else
            {
                vm = new EmployeeVm();
                vm.Id = 0;
                vm.LongName = string.Empty;
            }

            var itemDto = new AssetDTO();
            itemDto.Id = result.Id;

            itemDto.PartVmName = part?.Name ?? string.Empty;
            itemDto.PartVmId = part?.Id ?? 0;
            itemDto.partVm = _mapper.Map<PartVm>(part);
            itemDto.EndOfSupport = part.EndOfSupport;

            itemDto.InvoiceVmNumber = invoice?.Number ?? string.Empty;
            itemDto.InvoiceVmId = invoice?.Id ?? 0;
            itemDto.invoiceVm = _mapper.Map<InvoiceVm>(invoice);
            itemDto.InvoiceItemID = result.InvoiceItemId;

            itemDto.StateVmName = state?.Name ?? string.Empty;
            itemDto.StateVmId = state?.Id ?? 0;
            itemDto.stateVm = _mapper.Map<StateVm>(state);

            itemDto.WarehouseVmNumber = warehouse?.Number.ToString() ?? string.Empty;
            itemDto.WarehouseVmName = warehouse?.Name ?? string.Empty;
            itemDto.WarehouseVmId = warehouse?.Id ?? 0;
            itemDto.warehouseVm = _mapper.Map<WarehouseVm>(warehouse);

            itemDto.CurrencyVmName = currency?.Name ?? string.Empty;
            itemDto.CurrencyVmId = currency?.Id ?? 0;
            itemDto.currencyVm = _mapper.Map<CurrencyVm>(currency);

            itemDto.AssetTagNumber = result.AssetTagNumber.ToString();
            itemDto.SerialNumber = result.SerialNumber.ToString();
            itemDto.LastSeen = result.LastSeen;
            itemDto.Price = result.Price;
            itemDto.PurchaseDate = result.PurchaseDate;
            itemDto.WarrantyUntil = result.WarrantyUntil;
            itemDto.Imei = result.Imei;
            itemDto.Mac = result.Mac;

            itemDto.AssigneeVmId = result.AssigneeId;
            itemDto.AssigneeVmName = result.AssigneeName;
            itemDto.AssigneeVmType = result.AssigneeType;
            itemDto.AssigneeVM = vm;

        return itemDto;
    }
}
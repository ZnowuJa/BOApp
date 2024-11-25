using Application.DTOs;
using Application.Interfaces;
using Application.ViewModels.General;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Assets.Queries;
public class GetAllAssetsDTOQuery : IRequest<IQueryable<AssetDTO>>
{

}
public class GetAllAssetsDTOQueryHandler : IRequestHandler<GetAllAssetsDTOQuery, IQueryable<AssetDTO>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    //private readonly Stopwatch _stopwatch;
    public GetAllAssetsDTOQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        //_stopwatch = new Stopwatch();
    }
    public async Task<IQueryable<AssetDTO>> Handle(GetAllAssetsDTOQuery request, CancellationToken cancellationToken)
    {
        //_stopwatch.Start();
        //var stopwatch2 = new Stopwatch();
        //stopwatch2.Start();
        //var before = Process.GetCurrentProcess().VirtualMemorySize64;
        var listItems = new List<AssetDTO>();

        var parts = await _appDbContext.Parts.ToListAsync(cancellationToken);
        var invoices = await _appDbContext.Invoices.ToListAsync(cancellationToken);
        var states = await _appDbContext.States.ToListAsync(cancellationToken);
        var employees = await _appDbContext.Employees.ToListAsync(cancellationToken);
        var departments = await _appDbContext.Departments.ToListAsync(cancellationToken);
        var warehouses = await _appDbContext.Warehouses.ToListAsync(cancellationToken);
        var currencies = await _appDbContext.Currencies.ToListAsync(cancellationToken);

        var result = await _appDbContext.Assets.Where(p => p.StatusId == 1)
            .ToListAsync(cancellationToken);
        //stopwatch2.Stop();
        foreach (var item in result)
        {
            var part = parts.FirstOrDefault(p => p.Id == item.PartId);
            var invoice = invoices.FirstOrDefault(p => p.Id == item.InvoiceId);
            var state = states.FirstOrDefault(p => p.Id == item.StateId);
            var warehouse = warehouses.FirstOrDefault(p => p.Id == item.WarehouseId);
            var currency = currencies.FirstOrDefault(p => p.Id == item.CurrencyId);
            IAssigneeVm vm = null;
            if (item.AssigneeType == "DepartmentVm")
            {
                var dept = departments.FirstOrDefault(p => p.Id == item.AssigneeId);
                vm = _mapper.Map<DepartmentVm>(dept);

            }
            else if (item.AssigneeType == "EmployeeVm")
            {
                var empl = employees.FirstOrDefault(p => p.EnovaEmpId == item.AssigneeId);
                vm = _mapper.Map<EmployeeVm>(empl);

            }
            else
            {
                vm = new EmployeeVm();
                vm.Id = 0;
                vm.LongName = string.Empty;
            }

            var itemDto = new AssetDTO();
            itemDto.Id = item.Id;

            itemDto.PartVmName = part?.Name ?? string.Empty;
            itemDto.PartVmId = part?.Id ?? 0;
            itemDto.partVm = _mapper.Map<PartVm>(part);
            itemDto.EndOfSupport = part.EndOfSupport;

            itemDto.InvoiceVmNumber = invoice?.Number ?? string.Empty;
            itemDto.InvoiceVmId = invoice?.Id ?? 0;
            itemDto.invoiceVm = _mapper.Map<InvoiceVm>(invoice);
            itemDto.InvoiceItemID = item.InvoiceItemId;

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

            itemDto.AssetTagNumber = item.AssetTagNumber.ToString();
            itemDto.SerialNumber = item.SerialNumber.ToString();
            itemDto.LastSeen = item.LastSeen;
            itemDto.Price = item.Price;
            itemDto.PurchaseDate = item.PurchaseDate;
            itemDto.WarrantyUntil = item.WarrantyUntil;
            itemDto.Imei = item.Imei;
            itemDto.Mac = item.Mac;

            itemDto.AssigneeVmId = item.AssigneeId;
            itemDto.AssigneeVmName = item.AssigneeName;
            itemDto.AssigneeVmType = item.AssigneeType;
            itemDto.AssigneeVM = vm;
            listItems.Add(itemDto);
        }

        //_stopwatch.Stop();
        //var after =  Process.GetCurrentProcess().VirtualMemorySize64;

        //Console.WriteLine($"Cała Operacja trwała {_stopwatch.ElapsedMilliseconds} ms | Operacja wczytania bez pętli trwałą {stopwatch2.ElapsedMilliseconds} ms | Potrzebna pamięć: {after - before}");
        return listItems.AsQueryable();
    }
}
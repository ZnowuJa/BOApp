using Application.ViewModels;
using Application.ViewModels.Employees;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.ITWarehouseCQRS.AssetsById.Commands;
public class CreateAssetByIdCommand : IRequest<int>
{
    public int PartId { get; set; }
    public int InvoiceId { get; set; }
    public int StateId { get; set; }
    public string AssetTagNumber { get; set; }
    public string SerialNumber { get; set; }
    public DateTime LastSeen { get; set; }
    public int EmployeeId { get; set; }
    public int WarehouseId { get; set; }
    public decimal Price { get; set; }
    public int CurrencyId { get; set; }
    public DateTime? PurchaseDate { get; set; }

    public CreateAssetByIdCommand( int partVm, int invoiceVm, int state, string assetTagNumber, string serialNumber, DateTime lastSeen, int employeeVm, int warehouseVm, decimal price, int currencyVm, DateTime purchaseDate)
    {
        PartId = partVm;
        InvoiceId = invoiceVm;
        StateId = state;
        AssetTagNumber = assetTagNumber;
        SerialNumber = serialNumber;
        LastSeen = lastSeen;
        EmployeeId = employeeVm;
        WarehouseId = warehouseVm; 
        CurrencyId = currencyVm;
        PurchaseDate = purchaseDate;
    }
}
using Application.ExportModels;
using Application.Forms.IT;
using Application.Interfaces;
using Application.Mappings;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using FluentValidation;

namespace Application.DTOs;

public class AssetDTO : IMapFrom<Asset>
{
    public int Id { get; set; }
    public string PartVmName { get; set; }
    public int PartVmId { get; set; }
    public PartVm partVm { get; set; }
    public string InvoiceVmNumber { get; set; }
    public int InvoiceVmId { get; set; }
    public InvoiceVm invoiceVm { get; set; }
    public int InvoiceItemID {get; set; }
    public string StateVmName { get; set; }
    public int StateVmId { get; set; }
    public StateVm stateVm { get; set; }
    public string AssetTagNumber { get; set; }
    public string SerialNumber { get; set; }
    public DateTime? LastSeen { get; set; }
    public int? AssigneeVmId { get; set; }
    public string? AssigneeVmName { get; set; }
    public string? AssigneeVmType { get; set; }
    public IAssigneeVm? AssigneeVM { get; set; }
    public string? WarehouseVmName { get; set; }
    public string? WarehouseVmNumber { get; set; }
    public int? WarehouseVmId { get; set; }
    public WarehouseVm? warehouseVm { get; set; }
    public decimal Price { get; set; }
    public string CurrencyVmName { get; set; }
    public int CurrencyVmId { get; set; }
    public CurrencyVm currencyVm { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public bool Leasing {  get; set; }
    public int StatusId { get; set; }
    public DateTime? EndOfContract { get; set; }
    public DateTime? WarrantyUntil { get; set; }
    public string? Imei { get; set; }
    public string? Mac { get; set; }
    public DateTime? EndOfSupport { get; set; }
    public int? ScrappingFormId { get; set; }
    public int? SaleFormId { get; set; }
    public ITScrappingFormVm? ScrappingForm { get; set; }
    public ITSaleFormVm? SaleForm { get; set; }
    public string? ScrappingReason { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Asset, AssetDTO>().ReverseMap();
        profile.CreateMap<AssetDTO, AssetExportModel>()
                .ForMember(dest => dest.InvoiceDocNumber, opt => opt.MapFrom(src => src.invoiceVm.Number));
        //profile.CreateMap<AssetDTO, AssetMinimal>()
        //    .ForMember(dest => dest.ScrappingFormNumber, opt => opt.MapFrom(src => src.ScrappingForm.Number))
        //    .ForMember(dest => dest.SaleFormNumber, opt => opt.MapFrom(src => src.SaleForm.Number));
    }


    public bool Compare(AssetDTO a, AssetDTO b)
    {
        bool result = false;

        if (a == b)
        {  return true; }

        return result;
    }
    public AssetDTO()
    {
    
    }

    public AssetDTO(AssetDTO other)
    {
        Id = other.Id;
        PartVmName = other.PartVmName;
        PartVmId = other.PartVmId;
        partVm = other.partVm;
        InvoiceVmNumber = other.InvoiceVmNumber;
        InvoiceVmId = other.InvoiceVmId;
        invoiceVm = other.invoiceVm;
        InvoiceItemID = other.InvoiceItemID;
        StateVmName = other.StateVmName;
        StateVmId = other.StateVmId;
        stateVm = other.stateVm;
        AssetTagNumber = other.AssetTagNumber;
        SerialNumber = other.SerialNumber;
        LastSeen = other.LastSeen;
        AssigneeVmId = other.AssigneeVmId;
        AssigneeVmName = other.AssigneeVmName;
        AssigneeVmType = other.AssigneeVmType;
        AssigneeVM = other.AssigneeVM;
        WarehouseVmName = other.WarehouseVmName;
        WarehouseVmNumber = other.WarehouseVmNumber;
        WarehouseVmId = other.WarehouseVmId;
        warehouseVm = other.warehouseVm;
        Price = other.Price;
        CurrencyVmName = other.CurrencyVmName;
        CurrencyVmId = other.CurrencyVmId;
        currencyVm = other.currencyVm;
        PurchaseDate = other.PurchaseDate;
        Leasing = other.Leasing;
        StatusId = other.StatusId;
        EndOfContract = other.EndOfContract;
        WarrantyUntil = other.WarrantyUntil;
        Imei = other.Imei;
        Mac = other.Mac;
        EndOfSupport = other.EndOfSupport;
        SaleFormId = other.SaleFormId;
        ScrappingFormId = other.ScrappingFormId;
        ScrappingReason = other.ScrappingReason;
    }
}

public class AssetDTOValidator : AbstractValidator<AssetDTO>
{
    public AssetDTOValidator()
    {
        RuleFor(x => x.PartVmId).NotNull().NotEqual(0);
        RuleFor(x => x.InvoiceVmId).NotNull().NotEqual(0);
        RuleFor(x => x.InvoiceItemID).NotNull().NotEqual(0);
        RuleFor(x => x.StateVmId).NotNull().NotEqual(0);
        RuleFor(x => x.AssetTagNumber).NotNull().MinimumLength(4);
        RuleFor(x => x.SerialNumber).NotNull().MinimumLength(4);
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.CurrencyVmId).NotEqual(0);
    }

}
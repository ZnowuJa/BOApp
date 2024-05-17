using Application.Interfaces;
using Application.Mappings;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using FluentValidation;
using FluentValidation.Results;
using System;

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
    public DateTime LastSeen { get; set; }
    public int AssigneeVmId { get; set; }
    public string AssigneeVmName { get; set; }
    public string AssigneeVmType { get; set; }
    public IAssigneeVm AssigneeVM { get; set; }
    //public int EmployeeVmId { get; set; }
    //public string EmployeeVmName { get; set; }
    //public EmployeeVm employeeVm { get; set; }
    public string WarehouseVmName { get; set; }
    public string WarehouseVmNumber { get; set; }
    public int WarehouseVmId { get; set; }
    public WarehouseVm warehouseVm { get; set; }
    //public string departmentName { get; set; }
    //public string departmentNumber { get; set; }
    //public int departmentId { get; set; }
    //public DepartmentVm departmentVm { get; set; }
    public decimal Price { get; set; }
    public string CurrencyVmName { get; set; }
    public int CurrencyVmId { get; set; }
    public CurrencyVm currencyVm { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public DateTime WarrantyUntil {  get; set; }
    public bool Leasing {  get; set; }
    public int StatusId { get; set; }
    public DateTime EndOfContract { get; set; }
    public string Imei { get; set; }
    public string Mac { get; set; }
    public DateTime? EndOfSupport { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Asset, AssetDTO>().ReverseMap();
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
        RuleFor(x => x.WarehouseVmId).NotNull().NotEqual(0);
        RuleFor(x => x.AssetTagNumber).NotNull().MinimumLength(4);
        RuleFor(x => x.SerialNumber).NotNull().MinimumLength(4);
        //RuleFor(x => x.LastSeen).NotEmpty();
        //RuleFor(x => x.EmployeeVmId).NotEqual(0);
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.CurrencyVmId).NotEqual(0);
        RuleFor(x => x.PurchaseDate).NotEmpty();

    }

}
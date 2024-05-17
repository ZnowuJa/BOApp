﻿using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels;
public class AssetVm : IMapFrom<Asset>
{
    public int Id { get; set; }
    public int PartVmId { get; set; }
    public int InvoiceVmId { get; set; }
    public int StateVmId { get; set; }
    public string AssetTagNumber { get; set; }
    public string SerialNumber { get; set; }
    public DateTime LastSeen { get; set; }
    public int EmployeeVmId { get; set; }
    public int WarehouseVmId { get; set; }
    public decimal Price { get; set; }
    public int CurrencyVmId { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public int StatusId { get; set; }
    public bool Leasing { get; set; }
    public DateTime EndOfContract { get; set; }
    public DateTime WarrantyUntil { get; set; }
    public string Imei { get; set; }
    public string Mac { get; set; }
    public DateTime EndOfSupport { get; set; }

    public void Mapping(Profile profile)
    {
        profile.AllowNullCollections = true;
        profile.CreateMap<Asset, AssetVm>()
            //.ForMember(d => d.PartId, s => s.MapFrom(src => src.Part))
            //.ForMember(e => e.InvoiceVm, t => t.MapFrom(src2 => src2.Invoice))
            //.ForMember(f => f.StateVm, u => u.MapFrom(src3 => src3.State))
            //.ForMember(h => h.WarehouseVm, x => x.MapFrom(src5 => src5.Warehouse))
            //.ForMember(i => i.CurrencyVm, y => y.MapFrom(src6 => src6.Currency))
            //.ForMember(i => i.EmployeeVm, y => y.MapFrom(src6 => src6.Employee))
            .ReverseMap();
        
    }

    public AssetVm()
    {
        Id = 0;
        PartVmId = 0;
        InvoiceVmId = 0;
        StateVmId = 0;
        AssetTagNumber = string.Empty;
        SerialNumber = string.Empty;
        LastSeen = DateTime.Now;
        EmployeeVmId = 0;
        WarehouseVmId = 0;
        Price = 0;
        CurrencyVmId = 0;
        PurchaseDate = DateTime.Now;
        Leasing = false;
        EndOfContract = DateTime.Now;
        WarrantyUntil = DateTime.Now;
        Imei = string.Empty;
        Mac = string.Empty;

    }
}

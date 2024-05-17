﻿using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels;
public class InvoiceItemVm : IMapFrom<InvoiceItem>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PartVmId { get; set; }
    public string PartVmName {  get; set; }
    public PartVm PartVm { get; set; }
    public decimal Qty { get; set; }
    public int UnitVmId { get; set; }
    public string UnitVmName { get; set; }
    public UnitVm UnitVm { get; set; }
    public decimal UnitNetPrice { get; set; }
    public int CurrencyVmId { get; set; }
    public string CurrencyVmName { get; set; }
    public CurrencyVm CurrencyVm { get; set; }
    public int InvoiceVmId { get; set; }
    public bool AssetsGenerated { get; set; }
    public List<AssetVm> AssetsVm { get; set; }

    public InvoiceItemVm(InvoiceItemVm src)
    {
        Id = src.Id;
        Name = src.Name;
        PartVmId = src.PartVmId;
        PartVmName = src.PartVmName;
        PartVm = src.PartVm;
        Qty = src.Qty;
        UnitVm = src.UnitVm;
        UnitVmId = src.UnitVmId;
        UnitVmName = src.UnitVmName;
        UnitNetPrice = src.UnitNetPrice;
        CurrencyVmId = src.CurrencyVmId;
        CurrencyVmName = src.CurrencyVmName;
        CurrencyVm = src.CurrencyVm;
        InvoiceVmId = src.InvoiceVmId;
        AssetsGenerated = src.AssetsGenerated;
        AssetsVm = src.AssetsVm;
        
    }
    public InvoiceItemVm()
    {
        Reset();
    }

    public void Reset()
    {
        Id = 0;
        Name = "Enter...";
        PartVmId = 0;
        PartVmName = "Select...";
        PartVm = new PartVm();
        Qty = 0;
        UnitVmId = 1;
        UnitVmName = "pcs";
        UnitVm = new UnitVm();
        UnitNetPrice = 0;
        CurrencyVmId = 1;
        CurrencyVmName = "PLN";
        CurrencyVm = new CurrencyVm();
        InvoiceVmId = 0;
        AssetsGenerated = false;
        AssetsVm = new List<AssetVm>();
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<InvoiceItem, InvoiceItemVm>().ReverseMap();


    }
}

using Application.Forms.Accounting.Enums;
using Application.Mappings;
using AutoMapper;
using Domain.Entities.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting;

public class BusinessPartnerVm : IMapFrom<BusinessPartner>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? BankAccountNumber { get; set; } = string.Empty;
    public string? VatId { get; set; } = string.Empty;
    public string? SAPId { get; set; } = string.Empty;
    public string BankTransferType { get; set; } = string.Empty;
    public string BusinessPartnerType { get; set; } = string.Empty;
    public string? Location { get; set; } = string.Empty;
    public string SAPFormType { get; set; } = string.Empty;
    public string DefaultCurrency { get; set; } = string.Empty;
    public bool Selected { get; set; } = false;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BusinessPartner, BusinessPartnerVm>().ReverseMap();
    }
}
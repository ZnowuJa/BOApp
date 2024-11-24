using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels;
public class InvoiceVm : IMapFrom<Invoice>
{
    public int Id { get; set; } 
    public string Number { get; set; }
    public int CompanyVmId { get; set; }
    public string CompanyVmName {  get; set; }
    public CompanyVm CompanyVm { get; set; }
    public DateTime? Date { get; set; }
    public decimal TotalNet { get; set; }
    public int CurrencyVmId { get; set; }
    public string CurrencyVmName { get; set; }
    public CurrencyVm CurrencyVm { get; set; }
    public List<InvoiceItemVm> InvoiceItems { get; set; }
    public bool Saved { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Invoice, InvoiceVm>().ReverseMap();


    }

    public InvoiceVm()
    {

    }
    public InvoiceVm(InvoiceVm other)
    {
        Id = other.Id;
        Number = other.Number;
        CompanyVmId = other.CompanyVmId;
        CompanyVmName = other.CompanyVmName;
        CompanyVm = other.CompanyVm;
        Date = other.Date;
        TotalNet = other.TotalNet;
        CurrencyVmId = other.CurrencyVmId;
        CurrencyVmName = other.CurrencyVmName;
        CurrencyVm = other.CurrencyVm;
        InvoiceItems = other.InvoiceItems;
        Saved = other.Saved;
    }
}





using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.ITWarehouseCQRS.Companies.Commands;
public class CreateCompanyCommand : IRequest<int>
{
    public string FullName { get; set; }
    public string Name { get; set; }
    public string VATID { get; set; }
    public string Street { get; set; }
    public string Building { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }
    public string ContactPerson { get; set; }
    public string ContactPersonMobile { get; set; }
    public string ContactPersonEmail { get; set; }
    public int? StatusId { get; set; }
    public CompanyTypeVm? CompanyTypeVm { get; set; }

    public CreateCompanyCommand(string fullName, string name, string vATID, string street, string building, string city, string postalCode, string country, string countryCode, string contactPerson, string contactPersonMobile, string contactPersonEmail, CompanyTypeVm? companyTypeVm)
    {
        FullName = fullName; 
        Name = name; 
        VATID = vATID; 
        Street = street; 
        Building = building; 
        City = city; 
        PostalCode = postalCode; 
        Country = country; 
        CountryCode = countryCode; 
        ContactPerson = contactPerson; 
        ContactPersonMobile = contactPersonMobile; 
        ContactPersonEmail = contactPersonEmail;
        CompanyTypeVm = companyTypeVm;
    }
}

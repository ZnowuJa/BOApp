using BackOfficeApp_Domain.Common;

namespace Domain.Entities.ITWarehouse;

public class Company : AuditableEntity
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
    public string? ContactPerson { get; set; }
    public string? ContactPersonMobile { get; set; }
    public string? ContactPersonEmail { get; set; }
    public CompanyType CompanyType { get; set; }
    public string? DealerSAPNumber { get; set; }
    

}

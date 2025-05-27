using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Companies.Commands;
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

public class CreateCompanyCommandHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<CreateCompanyCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var itemField = await _appDbContext.CompanyTypes.Where(p => p.Id == request.CompanyTypeVm.Id).FirstOrDefaultAsync();
        Company item = new()
        {
            FullName = request.FullName,
            Name = request.Name,
            VATID = request.VATID,
            Street = request.Street,
            Building = request.Building,
            City = request.City,
            PostalCode = request.PostalCode,
            Country = request.Country,
            CountryCode = request.CountryCode,
            ContactPerson = request.ContactPerson,
            ContactPersonMobile = request.ContactPersonMobile,
            ContactPersonEmail = request.ContactPersonEmail,
            CompanyType = itemField
        };
        _appDbContext.Companies.Add(item);
        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}

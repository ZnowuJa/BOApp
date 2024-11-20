using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Companies.Commands;
public class UpdateCompanyCommand : IRequest<int>
{
    public int Id { get; set; }
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

    public UpdateCompanyCommand(int id, string name, string fullName, string vATID, string street, string building, string city, string postalCode, string country, string countryCode, string contactPerson, string contactPersonMobile, string contactPersonEmail, CompanyTypeVm? companyTypeVm)
    {
        Id = id;
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
public class UpdateCompanyCommandHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<UpdateCompanyCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<int> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var itemField = await _appDbContext.CompanyTypes.Where(p => p.Id == request.CompanyTypeVm.Id).FirstOrDefaultAsync();
        var item = await _appDbContext.Companies.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

        item.Id = request.Id;
        item.Name = request.Name;
        item.FullName = request.FullName;
        item.VATID = request.VATID;
        item.Street = request.Street;
        item.Building = request.Building;
        item.City = request.City;
        item.PostalCode = request.PostalCode;
        item.Country = request.Country;
        item.CountryCode = request.CountryCode;
        item.ContactPerson = request.ContactPerson;
        item.ContactPersonMobile = request.ContactPersonMobile;
        item.ContactPersonEmail = request.ContactPersonEmail;
        item.CompanyType = itemField;

        _appDbContext.Companies.Update(item);

        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}
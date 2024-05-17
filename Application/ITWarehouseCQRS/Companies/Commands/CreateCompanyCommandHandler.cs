using Application.Interfaces;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.ITWarehouseCQRS.Companies.Commands;
public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CreateCompanyCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

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

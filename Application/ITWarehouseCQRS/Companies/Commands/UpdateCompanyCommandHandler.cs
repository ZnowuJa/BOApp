using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Companies.Commands;
public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public UpdateCompanyCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
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

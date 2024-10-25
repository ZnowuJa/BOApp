using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using Domain.Entities.Accounting;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.Countries.Commands
{
    public class CreateCountryCommand : IRequest<int>
    {
        public CountryVm Country { get; set; }

        public CreateCountryCommand(CountryVm country)
        {
            Country = country;
        }
    }

    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
    {
        private readonly IAppDbContext _context;
        public IMapper _mapper { get; }

        public CreateCountryCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = new Country
            {
                Code = request.Country.Code,
                Name = request.Country.Name,
                IsEU = request.Country.IsEU,
                StatusId = 1
            };

            _context.Countries.Add(country);
            await _context.SaveChangesAsync(cancellationToken);

            return country.Id;
        }
    }
}

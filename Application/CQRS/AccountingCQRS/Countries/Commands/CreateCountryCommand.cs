using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using Domain.Entities.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.Countries.Commands
{
    public class CreateCountryCommand(CountryVm country) : IRequest<int>
    {
        public CountryVm Country { get; set; } = country;
    }

    public class CreateCountryCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<CreateCountryCommand, int>
    {
        private readonly IAppDbContext _context = context;
        public IMapper _mapper { get; } = mapper;

        public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = _mapper.Map<Country>(request.Country);
            country.StatusId = 1;
            _context.Countries.Add(country);
            await _context.SaveChangesAsync(cancellationToken);
            return country.Id;
        }
    }
}

using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.Countries.Commands
{
    public class UpdateCountryCommand(CountryVm country) : IRequest<int>
    {
        public CountryVm Country { get; set; } = country;
    }

    public class UpdateCountryCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<UpdateCountryCommand, int>
    {
        private readonly IAppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == request.Country.Id, cancellationToken);
            _mapper.Map(request.Country, country);
            await _context.SaveChangesAsync(cancellationToken);
            return country.Id;
        }
    }
}

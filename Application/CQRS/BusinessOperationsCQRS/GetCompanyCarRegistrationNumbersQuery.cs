using Application.Interfaces;
using Application.ViewModels.BusinessOperations;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.BusinessOperationsCQRS;

public class GetCompanyCarRegistrationNumbersQuery : IRequest<List<CompanyCarRegistrationNumberVm>>
{
}

public class GetCompanyCarRegistrationNumbersQueryHandler : IRequestHandler<GetCompanyCarRegistrationNumbersQuery, List<CompanyCarRegistrationNumberVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetCompanyCarRegistrationNumbersQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<List<CompanyCarRegistrationNumberVm>> Handle(GetCompanyCarRegistrationNumbersQuery request, CancellationToken cancellationToken)
    {
        var query = _appDbContext.CompanyCarRegistrationNumbers
                .Select(c => new CompanyCarRegistrationNumberVm
                {
                    RegistrationNumber = c.RegistrationNumber
                }).ToList();

        return query;
    }

}
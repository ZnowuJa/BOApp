using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.CompanyTypes.Queries;
public class GetCompanyTypeQuery : IRequest<CompanyTypeVm>
{
    public GetCompanyTypeQuery(int i)
    {
        CompanyTypeId = i;
    }

    public int CompanyTypeId { get; set; }
}


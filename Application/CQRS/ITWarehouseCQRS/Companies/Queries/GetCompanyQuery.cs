using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Companies.Queries;
public class GetCompanyQuery : IRequest<CompanyVm>
{
    public GetCompanyQuery(int i)
    {
        CompanyId = i;
    }
    public int CompanyId { get; set; }
}

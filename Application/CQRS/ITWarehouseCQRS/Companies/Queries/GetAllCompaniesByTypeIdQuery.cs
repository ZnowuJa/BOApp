using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Companies.Queries;
public class GetAllCompaniesByTypeIdQuery : IRequest<IQueryable<CompanyVm>>
{
    public int TypeId { get; set; }
    public GetAllCompaniesByTypeIdQuery(int id)
    {
        TypeId = id;
    }
}

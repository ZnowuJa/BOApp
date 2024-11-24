using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.CompanyTypes.Queries;
public class GetAllCompanyTypesQuery : IRequest<IQueryable<CompanyTypeVm>>
{
}


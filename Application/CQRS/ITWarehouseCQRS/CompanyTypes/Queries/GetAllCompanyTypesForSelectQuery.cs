using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.CompanyTypes.Queries;
public class GetAllCompanyTypesForSelectQuery : IRequest<IQueryable<CompanyTypeVm>>
{
}


using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Companies.Queries;
public class GetAllCompaniesForSelectQuery : IRequest<IQueryable<CompanyVm>>
{
}

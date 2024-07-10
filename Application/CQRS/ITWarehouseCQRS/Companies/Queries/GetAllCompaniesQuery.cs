using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Companies.Queries;
public class GetAllCompaniesQuery : IRequest<IQueryable<CompanyVm>>
{
}

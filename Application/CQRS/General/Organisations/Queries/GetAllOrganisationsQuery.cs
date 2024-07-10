using Application.Forms;
using Application.ViewModels;
using Application.ViewModels.General;

using MediatR;

namespace Application.CQRS.General.Organisations.Queries;
public class GetAllOrganisationsQuery : IRequest<IQueryable<OrganisationVm>>
{
}

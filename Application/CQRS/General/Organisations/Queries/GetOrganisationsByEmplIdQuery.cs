using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.General.Organisations.Queries;
public class GetOrganisationsByEmplIdQuery : IRequest<IQueryable<OrganisationVm>>
{
    public GetOrganisationsByEmplIdQuery(int i)
    {
        Id = i;
    }

    public int Id { get; set; }
}

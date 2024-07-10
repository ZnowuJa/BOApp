using Application.Forms;
using Application.ViewModels;
using Application.ViewModels.General;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.General.Organisations.Queries;
public class GetOrganisationQuery : IRequest<OrganisationVm>
{
    public GetOrganisationQuery(int i)
    {
        Id = i;
    }

    public int Id { get; set; }
}

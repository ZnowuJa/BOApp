using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels.General;

using MediatR;

namespace Application.CQRS.General.Organisations.Commands;
public class UpdateOrganisationCommand : IRequest<int>
{
    public OrganisationVm Item { get; set; }

    public UpdateOrganisationCommand(OrganisationVm organisationVm)
    {
        Item = organisationVm;
    }
}

using Application.Forms;
using Application.ViewModels.General;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.General.Organisations.Commands;
public class CreateOrganisationCommand : IRequest<int>
{
    public OrganisationVm Item { get; set; }

    public CreateOrganisationCommand(OrganisationVm item)
    {
        Item = item;
    }
}

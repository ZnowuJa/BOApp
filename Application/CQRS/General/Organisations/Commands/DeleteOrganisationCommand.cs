using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.General.Organisations.Commands;
public class DeleteOrganisationCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteOrganisationCommand(int id)
    {
        Id = id;
    }
}

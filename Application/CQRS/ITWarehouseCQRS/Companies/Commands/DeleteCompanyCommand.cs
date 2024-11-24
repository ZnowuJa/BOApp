using MediatR;

namespace Application.ITWarehouseCQRS.Companies.Commands;
public class DeleteCompanyCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteCompanyCommand(int id)
    {
        Id = id;
    }
}

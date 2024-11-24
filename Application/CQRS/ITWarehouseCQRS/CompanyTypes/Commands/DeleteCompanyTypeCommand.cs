using MediatR;

namespace Application.ITWarehouseCQRS.CompanyTypes.Commands;
public class DeleteCompanyTypeCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteCompanyTypeCommand(int id)
    {
        Id = id;
    }
}


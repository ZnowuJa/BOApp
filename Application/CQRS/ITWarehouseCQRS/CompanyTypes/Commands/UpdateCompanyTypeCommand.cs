using MediatR;

namespace Application.ITWarehouseCQRS.CompanyTypes.Commands;
public class UpdateCompanyTypeCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UpdateCompanyTypeCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }
}


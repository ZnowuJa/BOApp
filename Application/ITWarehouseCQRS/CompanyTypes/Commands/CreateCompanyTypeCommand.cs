using MediatR;

namespace Application.ITWarehouseCQRS.CompanyTypes.Commands;
public class CreateCompanyTypeCommand : IRequest<int>
{
    public string Name { get; set; }

    public CreateCompanyTypeCommand(string name)
    {
        Name = name;
    }
}


using MediatR;

namespace Application.ITWarehouseCQRS.Vendors.Commands;
public class CreateVendorCommand : IRequest<int>
{
    public string Name { get; set; }

    public CreateVendorCommand(string name)
    {
        Name = name;
    }
}

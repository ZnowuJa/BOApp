using MediatR;

namespace Application.ITWarehouseCQRS.Vendors.Commands;
public class UpdateVendorCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UpdateVendorCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
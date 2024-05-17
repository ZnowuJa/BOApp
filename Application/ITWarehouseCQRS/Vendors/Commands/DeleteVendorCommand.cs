using MediatR;

namespace Application.ITWarehouseCQRS.Vendors.Commands;
public class DeleteVendorCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteVendorCommand(int id)
    {
        Id = id;
    }
}
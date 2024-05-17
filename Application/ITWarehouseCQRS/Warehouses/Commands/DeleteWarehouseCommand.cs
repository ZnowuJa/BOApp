using MediatR;

namespace Application.ITWarehouseCQRS.Warehouses.Commands;
public class DeleteWarehouseCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteWarehouseCommand(int id)
    {
        Id = id;
    }
}

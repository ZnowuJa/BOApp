using MediatR;

namespace Application.ITWarehouseCQRS.Warehouses.Commands;
public class UpdateWarehouseCommand : IRequest<int>
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public UpdateWarehouseCommand(int id, int number, string name)
    {
        Id = id;
        Number = number;
        Name = name;
    }
}

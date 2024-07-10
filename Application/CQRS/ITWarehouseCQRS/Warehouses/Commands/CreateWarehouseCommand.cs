using MediatR;

namespace Application.ITWarehouseCQRS.Warehouses.Commands;
public class CreateWarehouseCommand : IRequest<int>
{
    public int Number { get; set; }
    public string Name { get; set; }

    public CreateWarehouseCommand(int number, string name)
    {
        Number = number;
        Name = name;
    }
}

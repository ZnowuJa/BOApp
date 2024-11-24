using MediatR;

namespace Application.ITWarehouseCQRS.Currencies.Commands;
public class UpdateCurrencyCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UpdateCurrencyCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

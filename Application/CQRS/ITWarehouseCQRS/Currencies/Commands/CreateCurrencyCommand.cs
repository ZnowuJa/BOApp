using MediatR;

namespace Application.ITWarehouseCQRS.Currencies.Commands;
public class CreateCurrencyCommand : IRequest<int>
{
    public string Name { get; set; }

    public CreateCurrencyCommand(string name)
    {
        Name = name;
    }
}
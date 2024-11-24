using MediatR;

namespace Application.ITWarehouseCQRS.Currencies.Commands;
public class DeleteCurrencyCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteCurrencyCommand(int id)
    {
        Id = id;
    }
}

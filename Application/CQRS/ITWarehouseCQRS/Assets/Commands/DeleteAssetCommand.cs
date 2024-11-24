using MediatR;

namespace Application.ITWarehouseCQRS.Assets.Commands;
public class DeleteAssetCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteAssetCommand(int id)
    {
        Id = id;
    }
}

using MediatR;

namespace Application.ITWarehouseCQRS.CategoryTypes.Commands;
public class DeleteCategoryTypeCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteCategoryTypeCommand(int id)
    {
        Id = id;
    }
}

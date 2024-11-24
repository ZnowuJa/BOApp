using MediatR;

namespace Application.ITWarehouseCQRS.Categories.Commands;
public class DeleteCategoryCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteCategoryCommand(int id)
    {
        Id = id;
    }
}
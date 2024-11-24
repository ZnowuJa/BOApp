using MediatR;

namespace Application.ITWarehouseCQRS.CategoryTypes.Commands;
public class UpdateCategoryTypeCommand : IRequest<int>
{
    public int Id { get; set; } 
    public string Name { get; set;}
    public UpdateCategoryTypeCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

using MediatR;

namespace Application.ITWarehouseCQRS.CategoryTypes.Commands;
public class CreateCategoryTypeCommand : IRequest<int>
{
    public string Name { get; set; }

    public CreateCategoryTypeCommand(string name)
    {
        Name = name;
    }
}

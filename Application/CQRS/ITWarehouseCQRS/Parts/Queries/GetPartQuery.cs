using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Parts.Queries;
public class GetPartQuery : IRequest<PartVm>
{
    public GetPartQuery(int i)
    {
        PartId = i;
    }
    public int PartId { get; set; }
}

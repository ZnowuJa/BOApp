using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Parts.Queries;
public class GetAllPartsQuery : IRequest<IQueryable<PartVm>>
{
}

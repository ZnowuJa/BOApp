using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Parts.Queries;
public class GetAllPartsForSelectQuery : IRequest<IQueryable<PartVm>>
{
}

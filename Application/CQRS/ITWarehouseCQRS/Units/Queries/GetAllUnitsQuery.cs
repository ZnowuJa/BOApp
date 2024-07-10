using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Units.Queries;
public class GetAllUnitsQuery : IRequest<IQueryable<UnitVm>>
{
}

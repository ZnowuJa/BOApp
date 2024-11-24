using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Units.Queries;
public class GetAllUnitsForSelectQuery : IRequest<IQueryable<UnitVm>>
{
}
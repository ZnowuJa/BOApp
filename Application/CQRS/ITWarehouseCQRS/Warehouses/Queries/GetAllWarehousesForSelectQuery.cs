using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Warehouses.Queries;
public class GetAllWarehousesForSelectQuery : IRequest<IQueryable<WarehouseVm>>
{
}

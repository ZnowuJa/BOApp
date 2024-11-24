using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Warehouses.Queries;
public class GetAllWarehousesQuery : IRequest<IQueryable<WarehouseVm>>
{
}

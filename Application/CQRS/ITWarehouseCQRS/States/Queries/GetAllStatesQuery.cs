using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.States.Queries;
public class GetAllStatesQuery : IRequest<IQueryable<StateVm>>
{
}

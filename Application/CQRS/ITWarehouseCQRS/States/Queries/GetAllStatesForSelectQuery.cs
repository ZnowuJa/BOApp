using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.States.Queries;
public class GetAllStatesForSelectQuery : IRequest<IQueryable<StateVm>>
{
}

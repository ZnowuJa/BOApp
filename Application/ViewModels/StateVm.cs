using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels;
public class StateVm : IMapFrom<State>
{ 
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? StatusId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<State, StateVm>().ReverseMap();
    }
}

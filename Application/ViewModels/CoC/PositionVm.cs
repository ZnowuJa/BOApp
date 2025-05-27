using Application.Mappings;

using AutoMapper;

using Domain.Entities.CoC;


namespace Application.ViewModels.CoC;
public class PositionVm : IMapFrom<Position>
{
    public int Id { get; set; }
    public int? StatusId { get; set; }
    public string? InactivatedBy { get; set; }
    public DateTime? Inactivated { get; set; }
    
    public string Name { get; set; }
    public string Cat { get; set; } = string.Empty;

    public int? GroupCoCId { get; set; } = 0;

    public GroupCoCVm? GroupCoC { get; set; }

    //private string Department {  get; set; }

    public PositionVm()
    {
        
    }
    public PositionVm(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Position, PositionVm>();
        profile.CreateMap<PositionVm, Position>();
    }
}

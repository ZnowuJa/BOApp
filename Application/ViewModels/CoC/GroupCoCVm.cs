using Application.Mappings;

using AutoMapper;

using Domain.Entities.CoC;

namespace Application.ViewModels.CoC;
public class GroupCoCVm : IMapFrom<GroupCoC>
{
    public int Id { get; set; }
    public int? StatusId { get; set; }
    public string? InactivatedBy { get; set; }
    public DateTime? Inactivated { get; set; }
    public string GroupName { get; set; }
    public ICollection<PositionVm> Positions { get; set; }
    public ICollection<InstructionCoCVm> Instructions { get; set; }

    public GroupCoCVm()
    {
        
    }
    public void AddPosition(PositionVm position)
    {
        Positions.Add(position);
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GroupCoC, GroupCoCVm>()
            .ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.Positions))
            .ForMember(dest => dest.Instructions, opt => opt.MapFrom(src => src.Instructions));
        profile.CreateMap<GroupCoCVm, GroupCoC>()
            .ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.Positions))
            .ForMember(dest => dest.Instructions, opt => opt.MapFrom(src => src.Instructions));
    }
}

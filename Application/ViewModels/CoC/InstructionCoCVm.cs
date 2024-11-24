using Application.Mappings;

using AutoMapper;

using Domain.Entities.CoC;

namespace Application.ViewModels.CoC;
public class InstructionCoCVm : IMapFrom<InstructionCoC>
{
    public int Id { get; set; }
    public int? StatusId { get; set; }
    public string? InactivatedBy { get; set; }
    public DateTime? Inactivated { get; set; }
    public string Title { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public DateOnly Published { get; set; }
    public string Link { get; set; }
    public string Colour { get; set; }
    public PriorityLevel Priority { get; set; }
    public ICollection<GroupCoCVm>? Groups { get; set; }

    public InstructionCoCVm()
    {
        
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<InstructionCoC, InstructionCoCVm>()
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (PriorityLevel)src.Priority));

        profile.CreateMap<InstructionCoCVm, InstructionCoC>()
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (int)src.Priority));
 

    }

    public void AddGroup(GroupCoCVm group)
    {
        Groups.Add(group);
    }

}

public enum PriorityLevel
{
    Highest = 0, High = 1, Medium = 2, Low = 3
}
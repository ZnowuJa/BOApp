using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels;
public class NoteVm : IMapFrom<Note>
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int? StatusId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Note, NoteVm>().ReverseMap();
    }
}

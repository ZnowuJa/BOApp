using Application.Mappings;
using AutoMapper;
using Domain.Entities.Administration;
using System.Text.Json;

namespace Application.ViewModels.General
{
    public class ManagerDeputyVm : IMapFrom<ManagerDeputy>
    {
        public int Id { get; set; }

        public int ManagerId { get; set; }

        public string LongName { get; set; }

        public List<OrganisationRoleVm>? Deputies { get; set; } = new();

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<ManagerDeputy, ManagerDeputyVm>()
                .ForMember(dest => dest.Deputies, opt => opt.MapFrom(src => DeserializeRoles(src.Deputies)));

            profile.CreateMap<ManagerDeputyVm, ManagerDeputy>()
                .ForMember(dest => dest.Deputies, opt => opt.MapFrom(src => SerializeRoles(src.Deputies)));
        }

        private static string SerializeRoles(List<OrganisationRoleVm> roles)
        { 
            return roles == null || roles.Count == 0 ? "[]" : JsonSerializer.Serialize(roles);
        }

        private static List<OrganisationRoleVm> DeserializeRoles(string json)
        {
            return string.IsNullOrEmpty(json) ? new List<OrganisationRoleVm>() : JsonSerializer.Deserialize<List<OrganisationRoleVm>>(json);
        }
    }
}

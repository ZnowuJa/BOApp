using Application.AdHocJobs;
using Application.Mappings;
using Application.ViewModels.General;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using Domain.Forms;

namespace Application.Forms;

public class FormHistoryVm : IMapFrom<FormHistory>
{
    public int Id { get; set; }
    public string FormNumber { get; set; }
    public DateTime ModifiedDate { get; set; }
    public int EnovaEmpId { get; set; }
    public EmployeeVm ModifiedBy { get; set; }
    public List<Dictionary<string, string>> ModifiedFields { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<FormHistoryVm, FormHistory>()
            .ForMember(dest => dest.ModifiedFields, opt => opt.MapFrom(src => AppUtils.SerializeStringDictionary(src.ModifiedFields)));
        
        profile.CreateMap<FormHistory, FormHistoryVm>()
            .ForMember(dest =>dest.ModifiedFields, opt => opt.MapFrom(src => AppUtils.DeSerializeStringDictionary(src.ModifiedFields)));
    }
    
}
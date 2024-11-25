using System.Text.Json;

using Application.DTOs;
using Application.ViewModels.General;

using AutoMapper;

namespace Application.AdHocJobs;
public static class AppUtils
{
    public static List<AssetMinimal> ConvertAssetDTO2Minimal(List<AssetDTO> assets, IMapper _mapper)
    {
        
        //var minimals = new List<AssetMinimal>();
        var minimals = _mapper.Map<List<AssetMinimal>>(assets);
        //here write a code for convertion.


        return minimals;
    }
    public static string SerializeFiles(List<FormFileVm> files)
    {
        return files == null || files.Count == 0 ? string.Empty : JsonSerializer.Serialize(files);
    }
    public static List<FormFileVm> DeserializeFiles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<FormFileVm>() : JsonSerializer.Deserialize<List<FormFileVm>>(json);
    }
    public static string SerializeApprovals(List<Approval> approvals)
    {
        return approvals == null || approvals.Count == 0 ? string.Empty : JsonSerializer.Serialize(approvals);
    }
    public static List<Approval> DeserializeApprovals(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<Approval>() : JsonSerializer.Deserialize<List<Approval>>(json);
    }
    public static string SerializeRoles(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? string.Empty : JsonSerializer.Serialize(roles);
    }

    public static List<OrganisationRoleForFormVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleForFormVm>() : JsonSerializer.Deserialize<List<OrganisationRoleForFormVm>>(json);
    }
}

    
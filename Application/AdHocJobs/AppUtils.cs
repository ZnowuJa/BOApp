using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using Application.DTOs;
using Application.ViewModels.General;

using AutoMapper;

namespace Application.AdHocJobs;
public static class AppUtils
{
    public static string SerializeStringDictionary(List<Dictionary<string, string>> dictionary)
    {
        return dictionary == null || dictionary.Count == 0 ? string.Empty : JsonSerializer.Serialize(dictionary);
    }
    public static T SafeDeserialize<T>(string jsonString)
    {
        try
        {

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(jsonString);
        }
        catch (JsonException)
        {
            return default;
        }
    }

    public static string SafeSerialize<T>(T obj)
    {
        try
        {
            return JsonSerializer.Serialize(obj);
        }
        catch (JsonException)
        {
            return string.Empty;
        }
    }
    public static List<Dictionary<string, string>> DeSerializeStringDictionary(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<Dictionary<string, string>>() : JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);
    }

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
    public static string SerializeApprovals(List<ApprovalVm> approvals)
    {
        return approvals == null || approvals.Count == 0 ? string.Empty : JsonSerializer.Serialize(approvals);
    }
    public static List<ApprovalVm> DeserializeApprovals(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<ApprovalVm>() : JsonSerializer.Deserialize<List<ApprovalVm>>(json);
    }
    public static string SerializeRoles(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? string.Empty : JsonSerializer.Serialize(roles);
    }

    public static List<OrganisationRoleForFormVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleForFormVm>() : JsonSerializer.Deserialize<List<OrganisationRoleForFormVm>>(json);
    }
    private static readonly HttpClient httpClient = new();
    public static async Task<bool> ValidateIbanAsync(string iban)
    {
        if (string.IsNullOrWhiteSpace(iban)) { return false; }

        string apiSecret = Environment.GetEnvironmentVariable("SEPATOOLS_API_SECRET");

        if (string.IsNullOrEmpty(apiSecret)) { return false; }

        try
        {
            var byteArray = Encoding.ASCII.GetBytes($"piapl_service:{apiSecret}");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response = await httpClient.GetAsync($"https://rest.sepatools.eu/validate_iban/{iban}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var apiResponse = SafeDeserialize<ApiResponse>(result);

            return apiResponse?.Result == "passed";
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public static DateTime GetClosestWednesday(DateTime today)
    {
        int daysUntilWednesday = ((int)DayOfWeek.Wednesday - (int)today.DayOfWeek + 7) % 7;
        return today.AddDays(daysUntilWednesday);
    }
}

    
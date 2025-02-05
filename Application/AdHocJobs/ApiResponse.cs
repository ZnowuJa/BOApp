using System.Text.Json.Serialization;

namespace Application.AdHocJobs
{
    public class ApiResponse
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}

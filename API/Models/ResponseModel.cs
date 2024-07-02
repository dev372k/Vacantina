using System.Diagnostics;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class ResponseModel
    {
        public bool Status { get; set; } = true;
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Data { get; set; }
    }
}

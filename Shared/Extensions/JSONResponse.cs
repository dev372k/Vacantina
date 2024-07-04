using Shared.Exceptions;
using System.Text.Json.Serialization;

namespace Shared.Extensions;

public static class JSONResponse
{
    public static async Task<ResponseModel> ToResponse(this Task task, bool status = true, int statusCode = 200, string message = "", object data = null)
    {
        await task;
        return new ResponseModel
        {
            Status = status,
            StatusCode = statusCode,
            Message = message,
            Data = data
        };
    }
}


public class ResponseModel
{
    public bool Status { get; set; } = true;
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = string.Empty;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object Data { get; set; }
}

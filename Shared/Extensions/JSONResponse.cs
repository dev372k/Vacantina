using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class JSONResponse
    {
        // Extension method for Task (asynchronous operations returning void)
        public static async Task<ResponseModel> ToResponse(this Task task, bool status = true, int statusCode = 200, string message = "", object data = null)
        {
            try
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
            catch (CustomException ex)
            {               
                return new ResponseModel
                {
                    Status = false,
                    StatusCode = (int)ex.StatusCode,
                    Message = ex.Message
                };
            }
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
}

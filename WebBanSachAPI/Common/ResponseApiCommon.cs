using Microsoft.AspNetCore.Mvc;

namespace WebBanSachAPI.Common
{
    public static class ResponseApiCommon
    {
        public static ObjectResult Success(object data = null, string message = "Success", int statusCode = 200)
        {
            return new ObjectResult(new ApiResponse<object>(data, message, statusCode)) { StatusCode = statusCode };
        }
        public static ObjectResult Error(string errorMessage, int statusCode = 500)
        {
            return new ObjectResult(new ApiResponse<string>(null, errorMessage, statusCode, false)) { StatusCode = statusCode };
        }
    }
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public bool Success { get; set; }

        public ApiResponse(T data, string message = "Success", int status = 200, bool success = true)
        {
            Data = data;
            Message = message;
            Status = status;
            Success = success;
        }
    }
}

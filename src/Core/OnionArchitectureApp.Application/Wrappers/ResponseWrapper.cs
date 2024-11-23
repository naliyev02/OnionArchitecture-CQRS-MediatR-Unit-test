using OnionArchitectureApp.Application.Exceptions.Common;
using System.Net;

namespace OnionArchitectureApp.Application.Wrappers;

public class ResponseWrapper<T>
{
    public bool? Success { get; set; }
    public T? Data { get; set; }
    public string? Exception { get; set; }
    public HttpStatusCode? StatusCode { get; set; }

    public static ResponseWrapper<T> SuccessResult(T data, HttpStatusCode statusCode)
    {
        return new ResponseWrapper<T>
        {
            Success = true,
            Data = data, 
            StatusCode = statusCode
        };
    }

    public static ResponseWrapper<T> ErrorResult(string? exception, HttpStatusCode statusCode)
    {
        return new ResponseWrapper<T>
        {
            Success = false,
            Exception = exception,
            StatusCode = statusCode
        };
    }

}

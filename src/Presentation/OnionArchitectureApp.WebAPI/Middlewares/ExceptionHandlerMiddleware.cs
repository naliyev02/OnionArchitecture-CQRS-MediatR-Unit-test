using OnionArchitectureApp.Application.Exceptions.Common;
using OnionArchitectureApp.Application.Wrappers;
using System.Net;

namespace OnionArchitectureApp.WebAPI.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);

            _logger.LogError("Message => " + ex.Message + ". StatusCode => " + context.Response.StatusCode);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode = (int)HttpStatusCode.InternalServerError;
        string message = "Internal Server Error";

        if (exception is BaseException baseException)
        {
            statusCode = baseException.StatusCode;
            message = baseException.Message;
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = await ResponseWrapper<BaseException>.ErrorResult(message, (HttpStatusCode)statusCode);

        await context.Response.WriteAsJsonAsync(response);
        await context.Response.CompleteAsync();
    }
}

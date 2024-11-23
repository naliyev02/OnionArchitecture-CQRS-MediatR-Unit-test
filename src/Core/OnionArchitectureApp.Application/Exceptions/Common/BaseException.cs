namespace OnionArchitectureApp.Application.Exceptions.Common;

public abstract class BaseException : Exception
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public BaseException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}

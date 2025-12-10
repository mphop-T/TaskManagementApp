namespace Application.Common;

public class Response
{
    public bool IsSuccess { get; set; }
    public List<string>? Message { get; set; }

    public static Response Success()
    {
        return new Response
        {
            IsSuccess = true
        };
    }
    public static Response Fail(List<string> messages)
    {
        return new Response
        {
            IsSuccess = false,
            Message = messages
        };
    }
    public static Response Fail(string message)
    {
        return new Response
        {
            IsSuccess = false,
            Message = new List<string> { message }
        };
    }
}

public class Response<T> : Response
{
    public T? Data { get; set; }
    public string? InfoMessage { get; set; }
    public static Response<T> Success(T data, string? message = null)
    {
        return new Response<T>
        {
            IsSuccess = true,
            Data = data,
            InfoMessage = message
        };
    }
    public new static Response<T> Fail(List<string> messages)
    {
        return new Response<T>
        {
            IsSuccess = false,
            Message = messages
        };
    }
    public new static Response<T> Fail(string message)
    {
        return new Response<T>
        {
            IsSuccess = false,
            Message = new List<string> { message }
        };
    }
}

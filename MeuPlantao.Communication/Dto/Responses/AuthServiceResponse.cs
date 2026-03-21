namespace MeuPlantao.Communication.Dto.Responses;

public class AuthServiceResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static AuthServiceResponse<T> Ok(T data)
    {
        return new AuthServiceResponse<T>
        {
            Success = true,
            StatusCode = 200,
            Data = data
        };
    }

    public static AuthServiceResponse<T> BadRequest(string message)
    {
        return new AuthServiceResponse<T>
        {
            Success = false,
            StatusCode = 400,
            Message = message
        };
    }

    public static AuthServiceResponse<T> Unauthorized(string message)
    {
        return new AuthServiceResponse<T>
        {
            Success = false,
            StatusCode = 401,
            Message = message
        };
    }

    public static AuthServiceResponse<T> Error(string message)
    {
        return new AuthServiceResponse<T>
        {
            Success = false,
            StatusCode = 500,
            Message = message
        };
    }
}

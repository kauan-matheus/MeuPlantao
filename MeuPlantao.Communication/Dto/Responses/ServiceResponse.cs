namespace MeuPlantao.Communication.Dto.Responses;

public class ServiceResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static ServiceResponse<T> Ok(T data)
    {
        return new ServiceResponse<T>
        {
            Success = true,
            StatusCode = 200,
            Data = data
        };
    }

    public static ServiceResponse<T> BadRequest(string message)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            StatusCode = 400,
            Message = message
        };
    }

    public static ServiceResponse<T> Unauthorized(string message)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            StatusCode = 401,
            Message = message
        };
    }

    public static ServiceResponse<T> Error(string message)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            StatusCode = 500,
            Message = message
        };
    }
}

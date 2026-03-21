namespace MeuPlantao.Communication.Dto.Responses;

public class ResponseAuthRegisterJson
{
    public string Message { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public ResponseAuthUserJson Usuario { get; set; } = new();
}

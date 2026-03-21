namespace MeuPlantao.Communication.Dto.Responses;

public class ResponseAuthLoginJson
{
    public string Token { get; set; } = string.Empty;
    public string ExpiresIn { get; set; } = string.Empty;
    public ResponseAuthUserJson Usuario { get; set; } = new();
}

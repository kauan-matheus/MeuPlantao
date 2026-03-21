namespace MeuPlantao.Communication.Dto.Responses;

public class ResponseAuthUserJson
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Role { get; set; }
}

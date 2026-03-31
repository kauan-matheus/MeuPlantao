using System.ComponentModel.DataAnnotations;

namespace MeuPlantao.Communication.Dto.Requests;

public class RequestAuthRegisterEnfermeiroJson
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Coren { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
}
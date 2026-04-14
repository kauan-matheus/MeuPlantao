using System.ComponentModel.DataAnnotations;

namespace MeuPlantao.Communication.Dto.Requests;

public class RequestAuthRegisterMedicoJson
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Crm { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
}
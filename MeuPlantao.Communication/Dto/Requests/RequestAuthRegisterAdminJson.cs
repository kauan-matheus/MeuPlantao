using System.ComponentModel.DataAnnotations;

namespace MeuPlantao.Communication.Dto.Requests;

public class RequestAuthRegisterAdminJson
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
    public string Password { get; set; } = string.Empty;
}

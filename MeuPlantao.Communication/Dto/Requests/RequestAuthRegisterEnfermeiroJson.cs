using System.ComponentModel.DataAnnotations;

namespace MeuPlantao.Communication.Dto.Requests;

public class RequestAuthRegisterEnfermeiroJson
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Coren é obrigatório")]
    [MaxLength(10)]
    public string Coren { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefone é obrigatório")]
    [MaxLength(9)]
    public string Telefone { get; set; } = string.Empty;
}
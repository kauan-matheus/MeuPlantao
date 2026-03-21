using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Communication.Dto.Requests;

// DTO para criação e edição de usuário pelo admin
// Separado do UserModel para não expor a entidade de domínio diretamente na API
public class RequestUserRegisterJson
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Role é obrigatória")]
    public RoleEnum Role { get; set; }

    public bool Active { get; set; } = true;
}
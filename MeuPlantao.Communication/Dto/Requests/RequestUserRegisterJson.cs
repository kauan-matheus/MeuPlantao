using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Communication.Dto.Requests;

// DTO para criação e edição de usuário pelo admin
// Separado do UserModel para não expor a entidade de domínio diretamente na API
public class RequestUserRegisterJson
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public RoleEnum Role { get; set; }
    public bool Active { get; set; } = true;
}
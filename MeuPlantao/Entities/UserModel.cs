using MeuPlantao.Services.Enums;

namespace MeuPlantao.Model;

public class UserModel
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public RoleEnum Role { get; set; }
    public bool Active { get; set; }
}


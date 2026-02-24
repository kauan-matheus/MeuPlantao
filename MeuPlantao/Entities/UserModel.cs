using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Entities;

public class UserModel
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    [MaxLength(100)]
    public string PasswordHash { get; set; } = string.Empty;
    public RoleEnum Role { get; set; }
    public bool Active { get; set; }
}


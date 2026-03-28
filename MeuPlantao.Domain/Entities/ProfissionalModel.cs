using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Domain.Entities;

// Modelo do banco de Profissional (enfermeiro, médico etc.)
public class ProfissionalModel
{
    public long Id { get; set; }

    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;
    public ProfissionalRoleEnum Role { get; set; }

    [MaxLength(10)]
    public string? Crm { get; set; }
    [MaxLength(10)]
    public string? Coren { get; set; }
    [MaxLength(9)]
    public string Telefone { get; set; } = string.Empty;

    public long UserId { get; set; } // FK para UserModel

    // null! indica ao compilador que este campo será preenchido pelo EF Core (não é realmente nulo em runtime)
    public UserModel User { get; set; } = null!;
}
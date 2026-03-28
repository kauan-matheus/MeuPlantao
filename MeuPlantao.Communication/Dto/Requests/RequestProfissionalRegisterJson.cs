using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Communication.Dto.Requests;

public class RequestProfissionalRegisterJson
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

    // Corrigido de int para long para ser consistente com o Id do UserModel
    public long UserId { get; set; }
}
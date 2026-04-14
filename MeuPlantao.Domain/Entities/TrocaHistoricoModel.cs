using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Domain.Entities;

public class TrocaHistoricoModel
{
    public long Id { get; set; }
    public long TrocaPlantaoId { get; set; }
    public TrocaPlantaoModel TrocaPlantao { get; set; } = null!;
    public EventoHistoricoEnum Evento { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public long UsuarioId { get; set; }
    public UserModel Usuario { get; set; } = null!;
    [MaxLength(100)]
    public string Observacao { get; set; } = string.Empty;

}
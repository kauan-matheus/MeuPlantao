using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Entities;

public class TrocaHistoricoModel
{
    public int Id { get; set; }
    public int TrocaPlantaoId { get; set; }
    public TrocaPlantaoModel TrocaPlantao { get; set; } = null!;
    public EventoHistoricoEnum Evento { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public int UsuarioId { get; set; }
    public UserModel Usuario { get; set; } = null!;
    [MaxLength(100)]
    public string Observacao { get; set; } = string.Empty;

}
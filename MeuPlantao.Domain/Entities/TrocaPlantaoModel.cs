using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Domain.Entities;

public class TrocaPlantaoModel
{
    public long Id { get; set; }
    public long PlantaoId { get; set; }
    public PlantaoModel Plantao { get; set; } = null!;

    public long SolicitanteId { get; set; }
    public ProfissionalModel Solicitante { get; set; } = null!;

    public long DestinatarioId { get; set; }
    public ProfissionalModel Destinatario { get; set; } = null!;

    public StatusTrocaPlantaoEnum Status { get; set; }
    [MaxLength(100)]
    public string Motivo { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
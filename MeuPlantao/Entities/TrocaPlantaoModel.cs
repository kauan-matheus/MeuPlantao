using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Entities;

public class TrocaPlantaoModel
{
    public int Id { get; set; }
    public int PlantaoId { get; set; }
    public PlantaoModel Plantao { get; set; } = null!;

    public int SolicitanteId { get; set; }
    public ProfissionalModel Solicitante { get; set; } = null!;

    public int DestinatarioId { get; set; }
    public ProfissionalModel Destinatario { get; set; } = null!;

    public StatusTrocaPlantaoEnum Status { get; set; }
    [MaxLength(100)]
    public string Motivo { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
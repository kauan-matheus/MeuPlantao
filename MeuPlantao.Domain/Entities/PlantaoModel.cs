using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Domain.Entities;

public class PlantaoModel
{
    public long Id { get; set; }
    public long SetorId { get; set; }
    public SetorModel Setor { get; set; } = null!;
    public long? ProfissionalResponsavelId { get; set; }
    public ProfissionalModel? ProfissionalResponsavel { get; set; }
    public long? SolicitanteId { get; set; }
    public ProfissionalModel? Solicitante { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public StatusPlantaoEnum Status { get; set; }
}
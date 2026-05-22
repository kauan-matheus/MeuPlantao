using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Domain.Entities;

public class PlantaoModel
{
    public long Id { get; set; }
    public long SetorId { get; set; }
    public SetorModel Setor { get; set; } = null!;
    public long? ProfissionalResponsavelId { get; set; }
    public ProfissionalModel? ProfissionalResponsavel { get; set; }
    public ICollection<SolicitacaoModel> Solicitacoes { get; set; } = new List<SolicitacaoModel>();
    public float Valor { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public StatusPlantaoEnum Status { get; set; }
}
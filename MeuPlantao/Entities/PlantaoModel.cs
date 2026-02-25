using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Entities;

public class PlantaoModel
{
    public int Id { get; set; }
    public int SetorId { get; set; }
    public SetorModel Setor { get; set; } = null!;
    public int ProfissionalResponsavelId { get; set; }
    public ProfissionalModel ProfissionalResponsavel { get; set; } = null!;
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public StatusPlantaoEnum Status { get; set; }
}
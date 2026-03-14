using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Communication.Dto.Requests;

public class RequestPlantaoRegisterJson
{
    public long Id { get; set; }
    public long SetorId { get; set; }
    public long ProfissionalResponsavelId { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public StatusPlantaoEnum Status { get; set; }
}

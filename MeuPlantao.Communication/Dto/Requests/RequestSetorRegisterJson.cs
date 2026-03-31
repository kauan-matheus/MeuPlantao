using System.ComponentModel.DataAnnotations;

namespace MeuPlantao.Communication.Dto.Requests;

// DTO para criação e edição de setor
// Separado do SetorModel para não expor a entidade de domínio diretamente na API
public class RequestSetorRegisterJson
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public long RepresentanteId { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace MeuPlantao.Communication.Dto.Requests;

// DTO para criação e edição de setor
// Separado do SetorModel para não expor a entidade de domínio diretamente na API
public class RequestSetorRegisterJson
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;
    [Required(ErrorMessage = "Representante é obrigatória")]
    public long RepresentanteId { get; set; }
}
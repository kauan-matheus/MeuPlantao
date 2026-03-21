using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Communication.Dto.Requests;

//nessa classe aq ta dando warning mas nn ta errado, é só o compilador reclamando por causa da desserialização
public class RequestPlantaoRegisterJson
{
    //  setters privados pois os DTOs só precisam ser escritos na deserialização (init)
    public long Id { get; set; }

    [Required(ErrorMessage = "SetorId é obrigatório")]
    public long SetorId { get; set; }

    [Required(ErrorMessage = "ProfissionalResponsavelId é obrigatório")]
    public long ProfissionalResponsavelId { get; set; }

    [Required(ErrorMessage = "Início é obrigatório")]
    public DateTime Inicio { get; set; }

    [Required(ErrorMessage = "Fim é obrigatório")]
    public DateTime Fim { get; set; }

    [Required(ErrorMessage = "Status é obrigatório")]
    public StatusPlantaoEnum Status { get; set; }
}
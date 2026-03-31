using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Communication.Dto.Requests;

//nessa classe aq ta dando warning mas nn ta errado, é só o compilador reclamando por causa da desserialização
public class RequestPlantaoRegisterJson
{
    //  setters privados pois os DTOs só precisam ser escritos na deserialização (init)
    public long Id { get; set; }
    public long SetorId { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
}
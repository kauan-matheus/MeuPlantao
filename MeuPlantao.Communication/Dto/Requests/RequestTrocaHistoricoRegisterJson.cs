using System.ComponentModel.DataAnnotations;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Communication.Dto.Requests
{
    public class RequestTrocaHistoricoRegisterJson
    {
        public long Id { get; set; }
        public long TrocaPlantaoId { get; set; }
        public EventoHistoricoEnum Evento { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public long UsuarioId { get; set; }
        [MaxLength(100)]
        public string Observacao { get; set; } = string.Empty;
    }
}
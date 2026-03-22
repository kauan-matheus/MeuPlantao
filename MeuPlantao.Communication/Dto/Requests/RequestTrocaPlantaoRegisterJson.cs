using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Communication.Dto.Requests
{
    public class RequestTrocaPlantaoRegisterJson
    {
        public long Id { get; set; }
        public long PlantaoId { get; set; }
        public long SolicitanteId { get; set; }
        public long DestinatarioId { get; set; }
        public StatusTrocaPlantaoEnum Status { get; set; }
        [MaxLength(100)]
        public string Motivo { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
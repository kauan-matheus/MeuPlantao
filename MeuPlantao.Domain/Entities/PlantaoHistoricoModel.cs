using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Enums;

namespace MeuPlantao.Domain.Entities
{
    public class PlantaoHistoricoModel
    {
        public long Id { get; set; }
        public long PlantaoId { get; set; }
        public PlantaoModel Plantao { get; set; } = null!;
        public EventoPlantaoHistoricoEnum Evento { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public long UsuarioId { get; set; }
        public UserModel Usuario { get; set; } = null!;
        [MaxLength(100)]
        public string Observacao { get; set; } = string.Empty;
    }
}
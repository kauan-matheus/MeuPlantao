using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPlantao.Domain.Entities
{
    public class SolicitacaoModel
    {
        public long Id { get; set; }
	
        public long PlantaoId { get; set; }
        public PlantaoModel Plantao { get; set; } = null!;
        public long ProfissionalId { get; set; }
        public ProfissionalModel Profissional { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
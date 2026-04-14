using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPlantao.Communication.Dto.Responses
{
    public class ResponsePlantaoJson
    {
        public long ProfissionalResponsavelId { get; set; }
        public long SetorId { get; set; }
        public float Valor { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
    }
}
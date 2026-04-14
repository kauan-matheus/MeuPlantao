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
        public long PlantaoId { get; set; }
        public string Motivo { get; set; } = string.Empty;
    }
}
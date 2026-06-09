using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPlantao.Communication.Dto.Responses
{
    public class ResponsePlantaoJson
    {
        public long Id { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Start { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Locale { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public float Value { get; set; }
        public string Responsable { get; set; } = string.Empty;
        public string? OnDuty { get; set; }
        public int Status { get; set; }
    }
}
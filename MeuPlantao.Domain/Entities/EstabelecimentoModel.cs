using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeuPlantao.Domain.Entities
{
    public class EstabelecimentoModel
    {
        public long Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; } = String.Empty;
    }
}
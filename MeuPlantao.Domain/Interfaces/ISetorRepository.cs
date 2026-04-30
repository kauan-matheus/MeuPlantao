using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Domain.Interfaces
{
    public interface ISetorRepository : IRepository
    {
        Task<EstabelecimentoModel?> ExisteEstabelecimento(string estabelecimentoNome);
    }
}
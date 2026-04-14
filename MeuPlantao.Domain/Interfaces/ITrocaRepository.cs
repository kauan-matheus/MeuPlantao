using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Domain.Interfaces
{
    public interface ITrocaRepository : IRepository
    {
        Task<TrocaPlantaoModel?> ConsultarTrocaCompleta(long id);

        Task<bool> CadastrarComHistorico(TrocaPlantaoModel troca, TrocaHistoricoModel historico);
        Task<bool> EditarComHistorico(TrocaPlantaoModel troca, TrocaHistoricoModel historico);
    }
}
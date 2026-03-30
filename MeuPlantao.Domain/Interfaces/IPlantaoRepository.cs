using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Domain.Interfaces
{
    public interface IPlantaoRepository : IRepository
    {
        Task<PlantaoModel?> ConsultarPlantaoCompleto(long id);
        Task<bool> CadastrarComHistorico(PlantaoModel plantao, PlantaoHistoricoModel historico);
        Task<bool> EditarComHistorico(PlantaoModel plantao, PlantaoHistoricoModel historico);
    }
}
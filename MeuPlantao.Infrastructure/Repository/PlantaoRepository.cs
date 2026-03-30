using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using MeuPlantao.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Infrastructure.Repository
{
    public class PlantaoRepository : Repository, IPlantaoRepository
    {
        public PlantaoRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<PlantaoModel?> ConsultarPlantaoCompleto(long id)
        {
            return await _appDbContext.Plantoes
                .Include(p => p.Setor)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<bool> CadastrarComHistorico(PlantaoModel plantao, PlantaoHistoricoModel historico)
        {
            
            // Salva o plantao primeiro para que o banco gere o Id dele
            await _appDbContext.Set<PlantaoModel>().AddAsync(plantao);

            // Usa o Id gerado do plantao como FK do historico
            historico.Plantao = plantao;
            await _appDbContext.Set<PlantaoHistoricoModel>().AddAsync(historico);

            return true;
        }

        public async Task<bool> EditarComHistorico(PlantaoModel plantao, PlantaoHistoricoModel historico)
        {
            
            // Salva o plantao primeiro para que o banco gere o Id dele
            _appDbContext.Set<PlantaoModel>().Update(plantao);

            // Usa o Id gerado do plantao como FK do historico
            historico.Plantao = plantao;
            await _appDbContext.Set<PlantaoHistoricoModel>().AddAsync(historico);

            return true;
            
        }
    }
}
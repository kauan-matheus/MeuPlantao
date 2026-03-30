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
    public class TrocaRepository : Repository, ITrocaRepository
    {
        public TrocaRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<TrocaPlantaoModel?> ConsultarTrocaCompleta(long id)
        {
            return await _appDbContext.TrocaPlantoes
                .Include(t => t.Plantao)
                    .ThenInclude(p => p.Setor)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> CadastrarComHistorico(TrocaPlantaoModel troca, TrocaHistoricoModel historico)
        {

            // Salva a troca primeiro para que o banco gere o Id dele
            await _appDbContext.Set<TrocaPlantaoModel>().AddAsync(troca);

            // Usa o Id gerado da troca como FK do historico
            historico.TrocaPlantao = troca;
            await _appDbContext.Set<TrocaHistoricoModel>().AddAsync(historico);

            return true;
        }

        public async Task<bool> EditarComHistorico(TrocaPlantaoModel troca, TrocaHistoricoModel historico)
        {
            // Salva a troca primeiro para que o banco gere o Id dele
            _appDbContext.Set<TrocaPlantaoModel>().Update(troca);

            // Usa o Id gerado da troca como FK do historico
            historico.TrocaPlantao = troca;
            await _appDbContext.Set<TrocaHistoricoModel>().AddAsync(historico);

            return true;
        }
        
    }
}
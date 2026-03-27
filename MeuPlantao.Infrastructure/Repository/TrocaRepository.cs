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
        
    }
}
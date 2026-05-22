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
    public class ProfRepository : Repository, IProfRepository
    {
        public ProfRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<ProfissionalModel?> ConsultarPorUserId(long id)
        {
            return await _appDbContext.Profissionais
                .FirstOrDefaultAsync(t => t.UserId == id);
        }

        public IQueryable<PlantaoModel> ConsultarPlantoes(long id)
        {

            return _appDbContext.Plantoes
                .Where(p => p.ProfissionalResponsavelId == id)
                .OrderBy(p => p.Inicio);
        }

        public IQueryable<PlantaoModel> ConsultarPlantoesSolicitados(long id)
        {
            return _appDbContext.Solicitacoes
                .Where(s => s.ProfissionalId == id)
                .Select(s => s.Plantao)
                .OrderByDescending(p => p.Inicio);

        }
        
    }
}
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
        
    }
}
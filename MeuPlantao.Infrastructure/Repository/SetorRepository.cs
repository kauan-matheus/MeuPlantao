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
    public class SetorRepository : Repository, ISetorRepository
    {
        public SetorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<EstabelecimentoModel?> ExisteEstabelecimento(string estabelecimentoNome)
        {
            return await _appDbContext.Estabelecimentos.FirstOrDefaultAsync(e => e.Nome == estabelecimentoNome);
        }
    }
}
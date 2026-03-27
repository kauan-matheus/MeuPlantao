using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Domain.Interfaces
{
    public interface IProfRepository : IRepository
    {
        Task<ProfissionalModel?> ConsultarPorUserId(long id);
    }
}
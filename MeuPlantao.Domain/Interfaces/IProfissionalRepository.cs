using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Domain.Interfaces
{
    public interface IProfissionalRepository
    {
        ICollection<ProfissionalModel> GetProfissionais();
        ProfissionalModel GetProfissionalById(int id);
        bool PostProfissional(ProfissionalModel profissional);
        bool PutProfissional(ProfissionalModel profissional);
        void DeleteProfissional(ProfissionalModel profissional);
    }
}
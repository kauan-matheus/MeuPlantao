using MeuPlantao.Infrastructure.Data;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;

namespace MeuPlantao.Infrastructure.Repository
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProfissionalRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public ICollection<ProfissionalModel> GetProfissionais()
        {
            var responce = _appDbContext.Profissionais.ToList();
            return responce;
        }

        public ProfissionalModel? GetProfissionalById(int id)
        {
            var responce = _appDbContext.Profissionais.Find(id);
            return responce;
        }

        public bool PostProfissional(ProfissionalModel profissional)
        {
            _appDbContext.Profissionais.Add(profissional);
            return save();
        }

        public bool PutProfissional(ProfissionalModel profissional)
        {
            _appDbContext.Profissionais.Update(profissional);
            return save();
        }

        public void DeleteProfissional(ProfissionalModel profissional)
        {
            _appDbContext.Profissionais.Remove(profissional);
            _appDbContext.SaveChanges();
        }

        public bool save()
        {
            var saved = _appDbContext.SaveChanges() > 0? true : false;
            return saved;
        }
    }
}
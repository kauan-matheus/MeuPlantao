using MeuPlantao.Infrastructure.Data;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryable<T> Consultar<T>() where T : class
        {
            return _appDbContext.Set<T>();
        }

        public T? ConsultarPorId<T>(long id) where T : class
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public bool Cadastrar<T>(T model) where T : class
        {
            _appDbContext.Set<T>().Add(model);
            return Save();
        }

        public bool Editar<T>(T model) where T : class
        {
            _appDbContext.Set<T>().Update(model);
            return Save();
        }

        public bool Excluir<T>(T model) where T : class
        {
            _appDbContext.Set<T>().Remove(model);
            return Save();
        }

        public bool Save()
        {
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
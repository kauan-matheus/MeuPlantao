using MeuPlantao.Infrastructure.Data;
using MeuPlantao.Domain.Entities;
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
            return _appDbContext.Set<T>().AsQueryable();
        }

        public async Task<T?> ConsultarPorId<T>(long id) where T : class
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> Cadastrar<T>(T model) where T : class
        {
            await _appDbContext.Set<T>().AddAsync(model);
            return await Save();
        }

        public async Task<bool> Editar<T>(T model) where T : class
        {
            _appDbContext.Set<T>().Update(model);
            return await Save();
        }

        public async Task<bool> Excluir<T>(T model) where T : class
        {
            _appDbContext.Set<T>().Remove(model);
            return await Save();
        }

        public async Task<UserModel?> ConsultarUsuarioPorEmail(string email)
        {
            return await _appDbContext.Usuarios
                .FirstOrDefaultAsync(usuario => usuario.Email == email);
        }

        public async Task<bool> ExisteUsuarioPorEmail(string email)
        {
            return await _appDbContext.Usuarios
                .AnyAsync(usuario => usuario.Email == email);
        }

        public async Task<bool> CadastrarUsuarioComProfissional(UserModel usuario, ProfissionalModel profissional)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                await _appDbContext.Usuarios.AddAsync(usuario);
                await _appDbContext.SaveChangesAsync();

                profissional.UserId = usuario.Id;
                await _appDbContext.Profissionais.AddAsync(profissional);
                await _appDbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> Save()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}

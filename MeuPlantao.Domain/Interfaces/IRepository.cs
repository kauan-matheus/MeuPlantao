// é pra poder deixar nulo, da pra registrar no csproj tbm
#nullable enable

using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Domain.Interfaces
{
    public interface IRepository
    {
        IQueryable<T> Consultar<T>() where T : class;

        // Retorna nullable pois o registro pode não existir
        Task<T?> ConsultarPorId<T>(long id) where T : class;

        Task<bool> Cadastrar<T>(T model) where T : class;
        Task<bool> Editar<T>(T model) where T : class;
        Task<bool> Excluir<T>(T model) where T : class;
    }
}
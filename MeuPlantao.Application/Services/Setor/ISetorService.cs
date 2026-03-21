using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.Setor
{
    public interface ISetorService
    {
        Task<List<SetorModel>> Consultar();

        // Nullable pois o setor pode não ser encontrado
        Task<SetorModel?> ConsultarId(long id);

        Task<bool> Cadastrar(RequestSetorRegisterJson setor);
        Task<bool> Editar(RequestSetorRegisterJson setor);

        // Nullable pois retorna null se o setor não existir
        Task<SetorModel?> Deletar(long id);
    }
}
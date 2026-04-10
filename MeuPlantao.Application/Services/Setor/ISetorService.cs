using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.Setor
{
    public interface ISetorService
    {
        Task<ServiceResponse<List<SetorModel>>> Consultar();

        // Nullable pois o setor pode não ser encontrado
        Task<ServiceResponse<SetorModel>> ConsultarId(long id);

        Task<ServiceResponse<bool>> Cadastrar(RequestSetorRegisterJson setor);
        Task<ServiceResponse<bool>> Editar(RequestSetorRegisterJson setor);

        // Nullable pois retorna null se o setor não existir
        Task<ServiceResponse<SetorModel>> Deletar(long id);
    }
}
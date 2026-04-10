using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.Plantao
{
    public interface IPlantaoService
    {
        Task<ServiceResponse<List<ResponsePlantaoJson>>> Consultar();

        // Nullable pois o plantão pode não ser encontrado
        Task<ServiceResponse<ResponsePlantaoJson>> ConsultarId(long id);

        Task<ServiceResponse<bool>> Cadastrar(RequestPlantaoRegisterJson plantao, long userId);
        Task<ServiceResponse<bool>> Editar(RequestPlantaoRegisterJson plantao, long userId);

        // Nullable pois retorna null se o plantão não existir
        Task<ServiceResponse<ResponsePlantaoJson>> Deletar(long id);

        Task<ServiceResponse<bool>> Solicitar(long id, long UserId);
        Task<ServiceResponse<bool>> AceitarSolicitacao(long id, long userId);
        Task<ServiceResponse<bool>> RecusarSolicitacao(long id, long userId);
    }
}
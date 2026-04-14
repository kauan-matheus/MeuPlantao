using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.Plantao
{
    public interface IPlantaoService
    {
        Task<ServiceResponse<List<PlantaoModel>>> Consultar();
        Task<ServiceResponse<List<SolicitacaoModel>>> ConsultarSolicitacoes(long id);
        // Nullable pois o plantão pode não ser encontrado
        Task<ServiceResponse<PlantaoModel>> ConsultarId(long id);

        Task<ServiceResponse<bool>> Cadastrar(RequestPlantaoRegisterJson plantao, long userId);
        Task<ServiceResponse<bool>> Editar(RequestPlantaoRegisterJson plantao, long userId);

        // Nullable pois retorna null se o plantão não existir
        Task<ServiceResponse<PlantaoModel>> Deletar(long id);

        Task<ServiceResponse<bool>> Solicitar(long id, long UserId);
        Task<ServiceResponse<bool>> AceitarSolicitacao(long id, long solicitanteId, long userId);
        Task<ServiceResponse<bool>> RecusarSolicitacao(long id, long solicitanteId, long userId);
    }
}
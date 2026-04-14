using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.Profissional
{
    public interface IProfissionalService
    {
        Task<ServiceResponse<List<ProfissionalModel>>> Consultar();

        // Nullable pois o profissional pode não ser encontrado
        Task<ServiceResponse<ProfissionalModel>> ConsultarId(long id);
        Task<ServiceResponse<ProfissionalModel>> ConsultarUserId(long id);

        Task<ServiceResponse<bool>> Cadastrar(RequestProfissionalRegisterJson profissional);
        Task<ServiceResponse<bool>> Editar(RequestProfissionalRegisterJson profissional);

        // Nullable pois retorna null se o profissional não existir
        Task<ServiceResponse<ProfissionalModel>> Deletar(long id);
    }
}
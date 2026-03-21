using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.Plantao
{
    public interface IPlantaoService
    {
        Task<List<PlantaoModel>> Consultar();

        // Nullable pois o plantão pode não ser encontrado
        Task<PlantaoModel?> ConsultarId(long id);

        Task<bool> Cadastrar(RequestPlantaoRegisterJson plantao);
        Task<bool> Editar(RequestPlantaoRegisterJson plantao);

        // Nullable pois retorna null se o plantão não existir
        Task<PlantaoModel?> Deletar(long id);
    }
}
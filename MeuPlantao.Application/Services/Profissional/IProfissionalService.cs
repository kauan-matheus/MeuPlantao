using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.Profissional
{
    public interface IProfissionalService
    {
        Task<List<ProfissionalModel>> Consultar();

        // Nullable pois o profissional pode não ser encontrado
        Task<ProfissionalModel?> ConsultarId(long id);

        Task<bool> Cadastrar(RequestProfissionalRegisterJson profissional);
        Task<bool> Editar(RequestProfissionalRegisterJson profissional);

        // Nullable pois retorna null se o profissional não existir
        Task<ProfissionalModel?> Deletar(long id);
    }
}
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.User
{
    public interface IUserService
    {
        Task<ServiceResponse<List<UserModel>>> Consultar();

        // Nullable pois o usuário pode não ser encontrado
        Task<ServiceResponse<UserModel>> ConsultarId(long id);

        Task<ServiceResponse<bool>> Cadastrar(RequestUserRegisterJson user);
        Task<ServiceResponse<bool>> Editar(RequestUserRegisterJson user);

        // Nullable pois retorna null se o usuário não existir
        Task<ServiceResponse<UserModel>> Deletar(long id);
    }
}
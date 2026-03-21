using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.User
{
    public interface IUserService
    {
        Task<List<UserModel>> Consultar();

        // Nullable pois o usuário pode não ser encontrado
        Task<UserModel?> ConsultarId(long id);

        Task<bool> Cadastrar(RequestUserRegisterJson user);
        Task<bool> Editar(RequestUserRegisterJson user);

        // Nullable pois retorna null se o usuário não existir
        Task<UserModel?> Deletar(long id);
    }
}
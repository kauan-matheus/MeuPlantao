using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Domain.Interfaces
{
    public interface IAuthRepository : IRepository
    {
        Task<UserModel?> ConsultarUsuarioPorEmail(string email);
        Task<bool> ExisteUsuarioPorEmail(string email);
        Task<bool> CadastrarUsuarioComProfissional(UserModel usuario, ProfissionalModel profissional);
    }
}
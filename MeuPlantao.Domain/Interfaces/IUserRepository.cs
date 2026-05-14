using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Domain.Interfaces
{
    public interface IUserRepository : IRepository
    {
        Task<UserModel?> ConsultarUsuarioPorEmail(string email);
        Task<bool> ExisteUsuarioPorEmail(string email);
        Task<bool> CadastrarUsuarioComProfissional(UserModel usuario, ProfissionalModel profissional);
    }
}
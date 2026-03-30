using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using MeuPlantao.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Infrastructure.Repository
{
    public class AuthRepository : Repository, IAuthRepository
    {
        public AuthRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<UserModel?> ConsultarUsuarioPorEmail(string email)
        {
            return await _appDbContext.Usuarios
                .FirstOrDefaultAsync(usuario => usuario.Email == email);
        }

        public async Task<bool> ExisteUsuarioPorEmail(string email)
        {
            return await _appDbContext.Usuarios
                .AnyAsync(usuario => usuario.Email == email);
        }

        // Cadastra usuário e profissional de forma atômica usando transaction:
        // ou os dois são salvos, ou nenhum é — evita dados inconsistentes no banco
        public async Task<bool> CadastrarUsuarioComProfissional(UserModel usuario, ProfissionalModel profissional)
        {
            // Salva o usuário primeiro para que o banco gere o Id dele
            await _appDbContext.Usuarios.AddAsync(usuario);

            // Usa o Id gerado do usuário como FK do profissional
            profissional.User = usuario;
            await _appDbContext.Profissionais.AddAsync(profissional);

            return true;
        }
    }
}
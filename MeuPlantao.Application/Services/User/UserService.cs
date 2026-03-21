using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserModel>> Consultar()
        {
            return await _repository.Consultar<UserModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<UserModel?> ConsultarId(long id)
        {
            return await _repository.ConsultarPorId<UserModel>(id);
        }

        public async Task<bool> Cadastrar(RequestUserRegisterJson user)
        {
            // Mapeia DTO para entidade — hash da senha deve ser feito aqui antes de salvar
            var novo = new UserModel
            {
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Role = user.Role,
                Active = user.Active
            };

            return await _repository.Cadastrar(novo);
        }

        public async Task<bool> Editar(RequestUserRegisterJson user)
        {
            var novo = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Role = user.Role,
                Active = user.Active
            };

            return await _repository.Editar(novo);
        }

        public async Task<UserModel?> Deletar(long id)
        {
            var existente = await ConsultarId(id);
            if (existente is null)
                return null;

            await _repository.Excluir(existente);
            return existente;
        }
    }
}
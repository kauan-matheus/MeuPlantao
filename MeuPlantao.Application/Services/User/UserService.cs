using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Interfaces;
using MeuPlantao.Domain.Entities;
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
            var responce = await _repository.Consultar<UserModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();

            return responce;
        }

        public async Task<UserModel> ConsultarId(long Id)
        {
            var responce = await _repository.ConsultarPorId<UserModel>(Id);
            return responce;
        }

        public async Task<bool> Cadastrar(UserModel user)
        {
            var responce = await _repository.Cadastrar<UserModel>(user);
            return responce;
        }

        public async Task<bool> Editar(UserModel user)
        {
            var responce = await _repository.Editar<UserModel>(user);
            return responce;
        }

        public async Task<UserModel?> Deletar(long Id)
        {
            var existente = await ConsultarId(Id);
            if (existente != null)
            {
                await _repository.Excluir<UserModel>(existente);
                return existente;
            }
            else
            {
                return null;
            }
        }
    }
}
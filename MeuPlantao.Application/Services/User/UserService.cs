using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unit;

        public UserService(IRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<ServiceResponse<List<UserModel>>> Consultar()
        {
            try
            {
                var data = await _repository.Consultar<UserModel>()
                    .OrderBy(p => p.Id)
                    .ToListAsync();

                return ServiceResponse<List<UserModel>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<UserModel>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<UserModel>> ConsultarId(long id)
        {
            try
            {
                var data = await _repository.ConsultarPorId<UserModel>(id);

                if (data is null)
                    return ServiceResponse<UserModel>.BadRequest("Usuário não encontrado");

                return ServiceResponse<UserModel>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UserModel>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Cadastrar(RequestUserRegisterJson user)
        {
            try
            {
                var novo = new UserModel
                {
                    Email = user.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    Role = user.Role,
                    Active = user.Active
                };

                await _repository.Cadastrar(novo);
                var result = await _unit.Commit();

                return ServiceResponse<bool>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Editar(RequestUserRegisterJson user)
        {
            try
            {
                var novo = new UserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    Role = user.Role,
                    Active = user.Active
                };

                await _repository.Editar(novo);
                var result = await _unit.Commit();

                return ServiceResponse<bool>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<UserModel>> Deletar(long id)
        {
            try
            {
                var existente = await _repository.ConsultarPorId<UserModel>(id);

                if (existente is null)
                    return ServiceResponse<UserModel>.BadRequest("Usuário não encontrado");

                await _repository.Excluir(existente);
                await _unit.Commit();

                return ServiceResponse<UserModel>.Ok(existente);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UserModel>.Error(ex.Message);
            }
        }
    }
}
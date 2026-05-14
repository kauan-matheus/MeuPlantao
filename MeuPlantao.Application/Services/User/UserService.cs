using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unit;
        private readonly S3Service _s3Service;

        public UserService(IUserRepository repository, IUnitOfWork unit, S3Service s3Service)
        {
            _repository = repository;
            _unit = unit;
            _s3Service = s3Service;
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

        public async Task<ServiceResponse<string>> UploadFotoPerfil(long id, IFormFile arquivo)
        {
            var usuario = await _repository.ConsultarPorId<UserModel>(id);

            if (usuario == null)
                return ServiceResponse<string>.BadRequest("Usuario nao existe");

            if (arquivo == null || arquivo.Length == 0)
                return ServiceResponse<string>.BadRequest("E necessario enviar uma imagem para fazer upload");

            var nomeArquivo =
                $"usuarios/{id}/" +
                $"{Guid.NewGuid()}-{arquivo.FileName}";

            var url =
                await _s3Service.UploadArquivo(
                    arquivo,
                    nomeArquivo);

            usuario.FotoPerfilUrl = url;

            await _unit.Commit();

            return ServiceResponse<string>.Ok(url);
        }
    }
}
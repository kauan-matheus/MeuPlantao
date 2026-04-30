using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.Setor
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _repository;
        private readonly IUnitOfWork _unit;

        public SetorService(ISetorRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<ServiceResponse<List<SetorModel>>> Consultar()
        {
            try
            {
                var data = await _repository.Consultar<SetorModel>()
                    .OrderBy(p => p.Id)
                    .ToListAsync();

                return ServiceResponse<List<SetorModel>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<SetorModel>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<SetorModel>> ConsultarId(long id)
        {
            try
            {
                var data = await _repository.ConsultarPorId<SetorModel>(id);

                if (data is null)
                    return ServiceResponse<SetorModel>.BadRequest("Setor não encontrado");

                return ServiceResponse<SetorModel>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SetorModel>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Cadastrar(RequestSetorRegisterJson setor, long userLogado)
        {
            try
            {
                var user = await _repository.ConsultarPorId<UserModel>(userLogado);
                long estabelecimentoId;

                if (user is null)
                    return ServiceResponse<bool>.BadRequest("Representante precisa ser um usuario existente");

                if (user.Role != RoleEnum.Gestor)
                    return ServiceResponse<bool>.BadRequest("Usuario que não é um gestor não pode representar um setor");
                
                var existeEstabelecimento = await _repository.ExisteEstabelecimento(setor.EstabelecimentoNome);

                if (existeEstabelecimento is null)
                {
                    var novoEstabelecimento = new EstabelecimentoModel
                    {
                        Nome = setor.EstabelecimentoNome
                    };

                    await _repository.Cadastrar(novoEstabelecimento);
                    await _unit.Commit();
                    estabelecimentoId = novoEstabelecimento.Id;
                }
                else
                    estabelecimentoId = existeEstabelecimento.Id;
                
                var novoSetor = new SetorModel
                {
                    Nome = setor.Nome,
                    RepresentanteId = userLogado,
                    EstabelecimentoId = estabelecimentoId
                };

                await _repository.Cadastrar(novoSetor);

                var result = await _unit.Commit();

                return ServiceResponse<bool>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Editar(RequestSetorRegisterJson setor, long userLogado)
        {
            try
            {
                var user = await _repository.ConsultarPorId<UserModel>(userLogado);

                if (user is null)
                    return ServiceResponse<bool>.BadRequest("Representante precisa ser um usuario existente");

                if (user.Role != RoleEnum.Gestor)
                    return ServiceResponse<bool>.BadRequest("Usuario que não é um gestor não pode representar um setor");

                var novo = new SetorModel
                {
                    Id = setor.Id,
                    Nome = setor.Nome,
                    RepresentanteId = userLogado
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

        public async Task<ServiceResponse<SetorModel>> Deletar(long id)
        {
            try
            {
                var existente = await _repository.ConsultarPorId<SetorModel>(id);

                if (existente is null)
                    return ServiceResponse<SetorModel>.BadRequest("Setor não encontrado");

                await _repository.Excluir(existente);
                await _unit.Commit();

                return ServiceResponse<SetorModel>.Ok(existente);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SetorModel>.Error(ex.Message);
            }
        }
    }
}
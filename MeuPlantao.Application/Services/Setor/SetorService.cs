using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.Setor
{
    public class SetorService : ISetorService
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unit;

        public SetorService(IRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<List<SetorModel>> Consultar()
        {
            return await _repository.Consultar<SetorModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<SetorModel?> ConsultarId(long id)
        {
            return await _repository.ConsultarPorId<SetorModel>(id);
        }

        public async Task<bool> Cadastrar(RequestSetorRegisterJson setor)
        {
            var user = await _repository.ConsultarPorId<UserModel>(setor.RepresentanteId);

            if (user is null)
                throw new Exception("Representante precisa ser um usuario existente");

            if (user.Role != RoleEnum.Gestor)
                throw new Exception("Usuario que não é um gestor não pode representar um setor");

            // Mapeia o DTO para a entidade de domínio — nunca expõe SetorModel diretamente na API
            var novo = new SetorModel
            {
                Nome = setor.Nome,
                RepresentanteId = setor.RepresentanteId
            };

            await _repository.Cadastrar(novo);
            return await _unit.Commit();
        }

        public async Task<bool> Editar(RequestSetorRegisterJson setor)
        {
            var user = await _repository.ConsultarPorId<UserModel>(setor.RepresentanteId);
            
            if (user is null)
                throw new Exception("Representante precisa ser um usuario existente");

            if (user.Role != RoleEnum.Gestor)
                throw new Exception("Usuario que não é um gestor não pode representar um setor");

            var novo = new SetorModel
            {
                Id = setor.Id,
                Nome = setor.Nome,
                RepresentanteId = setor.RepresentanteId
            };

            await _repository.Editar(novo);
            return await _unit.Commit();
        }

        public async Task<SetorModel?> Deletar(long id)
        {
            var existente = await ConsultarId(id);
            if (existente is null)
                return null;

            await _repository.Excluir(existente);
            await _unit.Commit();
            return existente;
        }
    }
}
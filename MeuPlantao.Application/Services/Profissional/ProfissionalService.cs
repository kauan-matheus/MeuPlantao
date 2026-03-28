using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.Profissional
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IProfRepository _repository;
        private readonly IUnitOfWork _unit;

        public ProfissionalService(IProfRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<List<ProfissionalModel>> Consultar()
        {
            return await _repository.Consultar<ProfissionalModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<ProfissionalModel?> ConsultarId(long id)
        {
            return await _repository.ConsultarPorId<ProfissionalModel>(id);
        }

        public async Task<ProfissionalModel?> ConsultarUserId(long id)
        {
            return await _repository.ConsultarPorUserId(id);
        }

        public async Task<bool> Cadastrar(RequestProfissionalRegisterJson profissional)
        {
            // Valida se o usuário vinculado existe antes de cadastrar o profissional
            var existente = await _repository.ConsultarPorId<UserModel>(profissional.UserId);
            if (existente is null)
                return false;

            var novo = new ProfissionalModel
            {
                Nome = profissional.Nome,
                Role = profissional.Role,
                Crm = profissional.Crm,
                Coren = profissional.Crm,
                Telefone = profissional.Telefone,
                UserId = profissional.UserId,
                User = existente,
            };

            await _repository.Cadastrar(novo);
            return await _unit.Commit();
        }

        public async Task<bool> Editar(RequestProfissionalRegisterJson profissional)
        {
            // Valida se o usuário vinculado existe antes de editar o profissional
            var existente = await _repository.ConsultarPorId<UserModel>(profissional.UserId);
            if (existente is null)
                return false;

            var novo = new ProfissionalModel
            {
                Id = profissional.Id,
                Nome = profissional.Nome,
                Role = profissional.Role,
                Crm = profissional.Crm,
                Coren = profissional.Crm,
                Telefone = profissional.Telefone,
                UserId = profissional.UserId,
                User = existente,
            };

            await _repository.Editar(novo);
            return await _unit.Commit();
        }

        public async Task<ProfissionalModel?> Deletar(long id)
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
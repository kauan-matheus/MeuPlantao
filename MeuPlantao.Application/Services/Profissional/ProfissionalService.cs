using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.Profissional
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IRepository _repository;

        public ProfissionalService(IRepository repository)
        {
            _repository = repository;
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

        public async Task<bool> Cadastrar(RequestProfissionalRegisterJson profissional)
        {
            // Valida se o usuário vinculado existe antes de cadastrar o profissional
            var existente = await _repository.ConsultarPorId<UserModel>(profissional.UserId);
            if (existente is null)
                return false;

            var novo = new ProfissionalModel
            {
                Nome = profissional.Nome,
                Crm = profissional.Crm,
                Telefone = profissional.Telefone,
                UserId = profissional.UserId,
                User = existente,
            };

            return await _repository.Cadastrar(novo);
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
                Crm = profissional.Crm,
                Telefone = profissional.Telefone,
                UserId = profissional.UserId,
                User = existente,
            };

            return await _repository.Editar(novo);
        }

        public async Task<ProfissionalModel?> Deletar(long id)
        {
            var existente = await ConsultarId(id);
            if (existente is null)
                return null;

            await _repository.Excluir(existente);
            return existente;
        }
    }
}
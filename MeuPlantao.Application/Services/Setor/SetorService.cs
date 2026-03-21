using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.Setor
{
    public class SetorService : ISetorService
    {
        private readonly IRepository _repository;

        public SetorService(IRepository repository)
        {
            _repository = repository;
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
            // Mapeia o DTO para a entidade de domínio — nunca expõe SetorModel diretamente na API
            var novo = new SetorModel
            {
                Nome = setor.Nome
            };

            return await _repository.Cadastrar(novo);
        }

        public async Task<bool> Editar(RequestSetorRegisterJson setor)
        {
            var novo = new SetorModel
            {
                Id = setor.Id,
                Nome = setor.Nome
            };

            return await _repository.Editar(novo);
        }

        public async Task<SetorModel?> Deletar(long id)
        {
            var existente = await ConsultarId(id);
            if (existente is null)
                return null;

            await _repository.Excluir(existente);
            return existente;
        }
    }
}
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.Plantao
{
    public class PlantaoService : IPlantaoService
    {
        private readonly IRepository _repository;

        public PlantaoService(IRepository repository)
        {
            _repository = repository;

        }

        public async Task<List<PlantaoModel>> Consultar()
        {
            return await _repository.Consultar<PlantaoModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<PlantaoModel?> ConsultarId(long id)
        {
            return await _repository.ConsultarPorId<PlantaoModel>(id);
        }

        public async Task<bool> Cadastrar(RequestPlantaoRegisterJson plantao)
        {
            // Valida se o setor e o profissional existem antes de criar o plantão
            var setorExistente = await _repository.ConsultarPorId<SetorModel>(plantao.SetorId);
            var profExistente = await _repository.ConsultarPorId<ProfissionalModel>(plantao.ProfissionalResponsavelId);

            if (setorExistente is null || profExistente is null)
                return false;

            var novo = new PlantaoModel
            {
                SetorId = plantao.SetorId,
                Setor = setorExistente,
                ProfissionalResponsavelId = plantao.ProfissionalResponsavelId,
                ProfissionalResponsavel = profExistente,
                Inicio = plantao.Inicio,
                Fim = plantao.Fim,
                Status = plantao.Status
            };

            return await _repository.Cadastrar(novo);
        }

        public async Task<bool> Editar(RequestPlantaoRegisterJson plantao)
        {
            // Valida se o setor e o profissional existem antes de editar o plantão
            var setorExistente = await _repository.ConsultarPorId<SetorModel>(plantao.SetorId);
            var profExistente = await _repository.ConsultarPorId<ProfissionalModel>(plantao.ProfissionalResponsavelId);

            if (setorExistente is null || profExistente is null)
                return false;

            var novo = new PlantaoModel
            {
                Id = plantao.Id,
                SetorId = plantao.SetorId,
                Setor = setorExistente,
                ProfissionalResponsavelId = plantao.ProfissionalResponsavelId,
                ProfissionalResponsavel = profExistente,
                Inicio = plantao.Inicio,
                Fim = plantao.Fim,
                Status = plantao.Status
            };

            return await _repository.Editar(novo);
        }

        public async Task<PlantaoModel?> Deletar(long id)
        {
            var existente = await ConsultarId(id);
            if (existente is null)
                return null;

            await _repository.Excluir(existente);
            return existente;
        }
    }
}
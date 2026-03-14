using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var responce = await _repository.Consultar<PlantaoModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();

            return responce;
        }

        public async Task<PlantaoModel> ConsultarId(long Id)
        {
            var responce = await _repository.ConsultarPorId<PlantaoModel>(Id);
            return responce;
        }

        public async Task<bool> Cadastrar(RequestPlantaoRegisterJson plantao)
        {
            var setorExistente = await _repository.ConsultarPorId<SetorModel>(plantao.SetorId);
            var profExistente = await _repository.ConsultarPorId<ProfissionalModel>(plantao.ProfissionalResponsavelId);
            if (setorExistente != null && profExistente != null){
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

                var responce = await _repository.Cadastrar<PlantaoModel>(novo);
                return responce;
            }

            return false;
        }

        public async Task<bool> Editar(RequestPlantaoRegisterJson plantao)
        {
            var setorExistente = await _repository.ConsultarPorId<SetorModel>(plantao.SetorId);
            var profExistente = await _repository.ConsultarPorId<ProfissionalModel>(plantao.ProfissionalResponsavelId);
            if (setorExistente != null && profExistente != null){
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

                var responce = await _repository.Editar<PlantaoModel>(novo);
                return responce;
            }

            return false;
        }

        public async Task<PlantaoModel?> Deletar(long Id)
        {
            var existente = await ConsultarId(Id);
            if (existente != null)
            {
                await _repository.Excluir<PlantaoModel>(existente);
                return existente;
            }
            else
            {
                return null;
            }
        }
    }
}
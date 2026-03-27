using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.PlantaoHistorico
{
    public class PlantaoHistoricoService : IPlantaoHistoricoService
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unit;

        public PlantaoHistoricoService(IRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<List<PlantaoHistoricoModel>> Consultar()
        {
            return await _repository.Consultar<PlantaoHistoricoModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<PlantaoHistoricoModel?> ConsultarId(long id)
        {
            return await _repository.ConsultarPorId<PlantaoHistoricoModel>(id);
        }

        public async Task<bool> Cadastrar(RequestPlantaoHistoricoRegisterJson plantao)
        {
            //Mapeia o DTO para a entidade de domínio — nunca expõe o Model diretamente na API
            var novo = new PlantaoHistoricoModel
            {
                PlantaoId = plantao.PlantaoId,
                Evento = plantao.Evento,
                UsuarioId = plantao.UsuarioId,
                Observacao = plantao.Observacao
            };

            return await _unit.Commit();
        }

        public async Task<bool> Editar(RequestPlantaoHistoricoRegisterJson plantao)
        {
            var novo = new PlantaoHistoricoModel
            {
                Id = plantao.Id,
                PlantaoId = plantao.PlantaoId,
                Evento = plantao.Evento,
                UsuarioId = plantao.UsuarioId,
                Observacao = plantao.Observacao
            };

            await _repository.Editar(plantao);
            return await _unit.Commit();
        }

        public async Task<PlantaoHistoricoModel?> Deletar(long id)
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
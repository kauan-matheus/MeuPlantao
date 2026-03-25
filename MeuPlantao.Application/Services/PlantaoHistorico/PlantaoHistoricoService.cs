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

        public PlantaoHistoricoService(IRepository repository)
        {
            _repository = repository;
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

        public async Task<bool> Cadastrar(RequestPlantaoHistoricoRegisterJson troca)
        {
            // Mapeia o DTO para a entidade de domínio — nunca expõe o Model diretamente na API
            var novo = new PlantaoHistoricoModel
            {
                PlantaoId = troca.PlantaoId,
                Evento = troca.Evento,
                UsuarioId = troca.UsuarioId,
                Observacao = troca.Observacao
            };

            return await _repository.Cadastrar(novo);
        }

        public async Task<bool> Editar(RequestPlantaoHistoricoRegisterJson troca)
        {
            var novo = new PlantaoHistoricoModel
            {
                Id = troca.Id,
                PlantaoId = troca.PlantaoId,
                Evento = troca.Evento,
                UsuarioId = troca.UsuarioId,
                Observacao = troca.Observacao
            };

            return await _repository.Editar(novo);
        }

        public async Task<PlantaoHistoricoModel?> Deletar(long id)
        {
            var existente = await ConsultarId(id);
            if (existente is null)
                return null;

            await _repository.Excluir(existente);
            return existente;
        }
    }
}
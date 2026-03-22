using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.TrocaPlantao
{
    public class TrocaPlantaoService : ITrocaPlantaoService
    {
        private readonly IRepository _repository;

        public TrocaPlantaoService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TrocaPlantaoModel>> Consultar()
        {
            return await _repository.Consultar<TrocaPlantaoModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<TrocaPlantaoModel?> ConsultarId(long id)
        {
            return await _repository.ConsultarPorId<TrocaPlantaoModel>(id);
        }

        public async Task<bool> Cadastrar(RequestTrocaPlantaoRegisterJson troca)
        {
            // Mapeia o DTO para a entidade de domínio — nunca expõe o Model diretamente na API
            var novo = new TrocaPlantaoModel
            {
                PlantaoId = troca.PlantaoId,
                SolicitanteId = troca.SolicitanteId,
                DestinatarioId = troca.DestinatarioId,
                Status = troca.Status,
                Motivo = troca.Motivo,
                CreatedAt = troca.CreatedAt
            };

            return await _repository.Cadastrar(novo);
        }

        public async Task<bool> Editar(RequestTrocaPlantaoRegisterJson troca)
        {
            var novo = new TrocaPlantaoModel
            {
                Id = troca.Id,
                PlantaoId = troca.PlantaoId,
                SolicitanteId = troca.SolicitanteId,
                DestinatarioId = troca.DestinatarioId,
                Status = troca.Status,
                Motivo = troca.Motivo,
                CreatedAt = troca.CreatedAt
            };

            return await _repository.Editar(novo);
        }

        public async Task<TrocaPlantaoModel?> Deletar(long id)
        {
            var existente = await ConsultarId(id);
            if (existente is null)
                return null;

            await _repository.Excluir(existente);
            return existente;
        }
    }
}
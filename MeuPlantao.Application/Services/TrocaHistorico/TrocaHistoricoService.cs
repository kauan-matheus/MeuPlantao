using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.TrocaHistorico
{
    public class TrocaHistoricoService : ITrocaHistoricoService
    {
        private readonly IRepository _repository;

        public TrocaHistoricoService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TrocaHistoricoModel>> Consultar()
        {
            return await _repository.Consultar<TrocaHistoricoModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<TrocaHistoricoModel?> ConsultarId(long id)
        {
            return await _repository.ConsultarPorId<TrocaHistoricoModel>(id);
        }

        public async Task<bool> Cadastrar(RequestTrocaHistoricoRegisterJson troca)
        {
            // Mapeia o DTO para a entidade de domínio — nunca expõe o Model diretamente na API
            var novo = new TrocaHistoricoModel
            {
                TrocaPlantaoId = troca.TrocaPlantaoId,
                Evento = troca.Evento,
                UsuarioId = troca.UsuarioId,
                Observacao = troca.Observacao
            };

            return await _repository.Cadastrar(novo);
        }

        public async Task<bool> Editar(RequestTrocaHistoricoRegisterJson troca)
        {
            var novo = new TrocaHistoricoModel
            {
                Id = troca.Id,
                TrocaPlantaoId = troca.TrocaPlantaoId,
                Evento = troca.Evento,
                UsuarioId = troca.UsuarioId,
                Observacao = troca.Observacao
            };

            return await _repository.Editar(novo);
        }

        public async Task<TrocaHistoricoModel?> Deletar(long id)
        {
            var existente = await ConsultarId(id);
            if (existente is null)
                return null;

            await _repository.Excluir(existente);
            return existente;
        }
    }
}
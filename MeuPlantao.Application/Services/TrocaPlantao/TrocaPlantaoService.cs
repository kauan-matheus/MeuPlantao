using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Application.Services.TrocaHistorico;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.TrocaPlantao
{
    public class TrocaPlantaoService : ITrocaPlantaoService
    {
        private readonly IRepository _repository;
        private readonly ITrocaHistoricoService _historicoService;


        public TrocaPlantaoService(IRepository repository, ITrocaHistoricoService historicoService)
        {
            _repository = repository;
            _historicoService = historicoService;
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

        public async Task<bool> Cadastrar(RequestTrocaPlantaoRegisterJson troca, long userId)
        {
            var plantao = await _repository
                .ConsultarPorId<PlantaoModel>(troca.PlantaoId);

            if (plantao == null)
            {
                throw new Exception("plantao não existe");
            }

            if (plantao.Status != StatusPlantaoEnum.Ativo)
            {
                throw new Exception("Plantao não possui responsavel para que possa ocorrer uma troca");
            }

            // Mapeia o DTO para a entidade de domínio — nunca expõe o Model diretamente na API
            var novo = new TrocaPlantaoModel
            {
                PlantaoId = troca.PlantaoId,
                SolicitanteId = userId,
                DestinatarioId = troca.DestinatarioId,
                Status = troca.Status,
                Motivo = troca.Motivo,
                CreatedAt = troca.CreatedAt
            };

            var response1 = await _repository.Cadastrar(novo);
            
            var response2 = await _historicoService.Cadastrar(new RequestTrocaHistoricoRegisterJson
            {
                TrocaPlantaoId = novo.Id,
                Evento = EventoHistoricoEnum.Criada,
                UsuarioId = userId,
                Observacao = "Troca criada"
            });


            return response1 && response2;
        }

        public async Task<bool> Aceitar(long id, long userId)
        {
            var troca = await _repository
                .ConsultarPorId<TrocaPlantaoModel>(id);

            if (troca == null)
                return false;

            if (userId != troca.DestinatarioId)
                throw new Exception("Apenas o destinatário pode aceitar");

            if (troca.Status != StatusTrocaPlantaoEnum.Pendente)
                throw new Exception("Troca não pode ser aceita");

            troca.Status = StatusTrocaPlantaoEnum.Aceita;

            var response1 = await _repository.Editar(troca);

            var response2 = await _historicoService.Cadastrar(new RequestTrocaHistoricoRegisterJson
            {
                TrocaPlantaoId = troca.Id,
                Evento = EventoHistoricoEnum.Aceita,
                UsuarioId = userId,
                Observacao = "Destinatário aceitou"
            });

            return response1 && response2;
        }

        public async Task<bool> Recusar(long id, long userId)
        {
            var troca = await _repository
                .ConsultarPorId<TrocaPlantaoModel>(id);

            if (troca == null)
                return false;

            if (userId != troca.DestinatarioId)
                throw new Exception("Apenas o destinatário pode recusar");

            if (troca.Status != StatusTrocaPlantaoEnum.Pendente)
                throw new Exception("Troca não pode ser recusada");

            troca.Status = StatusTrocaPlantaoEnum.Recusada;

            var response1 = await _repository.Editar(troca);

            var response2 = await _historicoService.Cadastrar(new RequestTrocaHistoricoRegisterJson
            {
                TrocaPlantaoId = troca.Id,
                Evento = EventoHistoricoEnum.Recusada,
                UsuarioId = userId,
                Observacao = "Destinatário recusou"
            });

            return response1 && response2;
        }

        public async Task<bool> EnviarParaAprovacao(long id, long userId)
        {
            var troca = await _repository
                .ConsultarPorId<TrocaPlantaoModel>(id);

            if (troca == null)
                return false;

            if (troca.SolicitanteId != userId)
                throw new Exception("Apenas o solicitante pode enviar para aprovação");

            if (troca.Status != StatusTrocaPlantaoEnum.Aceita)
                throw new Exception("Troca precisa estar aceita");


            troca.Status = StatusTrocaPlantaoEnum.AguardandoAprovacao;

            var response1 = await _repository.Editar(troca);

            var response2 = await _historicoService.Cadastrar(new RequestTrocaHistoricoRegisterJson
            {
                TrocaPlantaoId = troca.Id,
                Evento = EventoHistoricoEnum.AguardandoAprovacao,
                UsuarioId = userId,
                Observacao = "Enviado para aprovação"
            });

            return response1 && response2;
        }

        public async Task<bool> Aprovar(long id, long userId)
        {
            var troca = await _repository
                .ConsultarPorId<TrocaPlantaoModel>(id);

            if (troca == null)
                return false;

            if (troca.SolicitanteId != userId)
                throw new Exception("Apenas o solicitante pode aprovar");

            if (troca.Status != StatusTrocaPlantaoEnum.AguardandoAprovacao)
                throw new Exception("Troca não está aguardando aprovação");


            troca.Status = StatusTrocaPlantaoEnum.Aprovada;

            var response1 = await _repository.Editar(troca);

            var response2 = await _historicoService.Cadastrar(new RequestTrocaHistoricoRegisterJson
            {
                TrocaPlantaoId = troca.Id,
                Evento = EventoHistoricoEnum.Aprovada,
                UsuarioId = userId,
                Observacao = "Aprovada"
            });

            return response1 && response2;
        }

        public async Task<bool> Reprovar(long id, long userId)
        {
            var troca = await _repository
                .ConsultarPorId<TrocaPlantaoModel>(id);

            if (troca == null)
                return false;

            if (troca.SolicitanteId != userId)
                throw new Exception("Apenas o solicitante pode reprovar");

            if (troca.Status != StatusTrocaPlantaoEnum.AguardandoAprovacao)
                throw new Exception("Troca não está aguardando aprovação");

            troca.Status = StatusTrocaPlantaoEnum.Reprovada;

            var response1 = await _repository.Editar(troca);

            var response2 = await _historicoService.Cadastrar(new RequestTrocaHistoricoRegisterJson
            {
                TrocaPlantaoId = troca.Id,
                Evento = EventoHistoricoEnum.Reprovada,
                UsuarioId = userId,
                Observacao = "Reprovada"
            });

            return response1 && response2;
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
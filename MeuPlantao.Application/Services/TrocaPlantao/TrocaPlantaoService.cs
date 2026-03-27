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
        private readonly ITrocaRepository _repository;
        private readonly IUnitOfWork _unit;
        private readonly IProfRepository _profRepository;


        public TrocaPlantaoService(ITrocaRepository repository, IUnitOfWork unit, IProfRepository service)
        {
            _repository = repository;
            _unit = unit;
            _profRepository = service;
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
        
            var prof = await _profRepository.ConsultarPorUserId(userId);

            if (plantao is null)
                throw new Exception("plantao não existe");
            if (prof is null)
                throw new Exception("É necessario estar logado como um profissional");

            if (plantao.ProfissionalResponsavelId is null)
                throw new Exception("Plantao não possui responsavel para que possa ocorrer uma troca");
            if (prof.Id == plantao.ProfissionalResponsavelId)
                throw new Exception("Destinatario é mesmo usuario que solicitante");

            // Mapeia o DTO para a entidade de domínio — nunca expõe o Model diretamente na API
            var novo = new TrocaPlantaoModel
            {
                PlantaoId = troca.PlantaoId,
                SolicitanteId = prof.Id,
                DestinatarioId = (long)plantao.ProfissionalResponsavelId,
                Status = troca.Status,
                Motivo = troca.Motivo,
                CreatedAt = troca.CreatedAt
            };

            await _repository.Cadastrar(novo);
            
            await _repository.Cadastrar(new TrocaHistoricoModel
            {
                TrocaPlantao = novo,
                Evento = EventoHistoricoEnum.Criada,
                UsuarioId = userId,
                Observacao = "Troca criada"
            });


            return await _unit.Commit();
        }

        public async Task<bool> Aceitar(long id, long userId)
        {
            var troca = await _repository
                .ConsultarPorId<TrocaPlantaoModel>(id);

            if (troca is null)
                return false;

            if (userId != troca.DestinatarioId)
                throw new Exception("Apenas o destinatário pode aceitar");

            if (troca.Status != StatusTrocaPlantaoEnum.Pendente)
                throw new Exception("Troca não pode ser aceita");

            troca.Status = StatusTrocaPlantaoEnum.Aceita;

            await _repository.Editar(troca);

            await _repository.Cadastrar(new TrocaHistoricoModel
            {
                TrocaPlantao = troca,
                Evento = EventoHistoricoEnum.Aceita,
                UsuarioId = userId,
                Observacao = "Destinatário aceitou"
            });

            return await _unit.Commit();
        }

        public async Task<bool> Recusar(long id, long userId)
        {
            var troca = await _repository
                .ConsultarPorId<TrocaPlantaoModel>(id);

            if (troca is null)
                return false;

            if (userId != troca.DestinatarioId)
                throw new Exception("Apenas o destinatário pode recusar");

            if (troca.Status != StatusTrocaPlantaoEnum.Pendente)
                throw new Exception("Troca não pode ser recusada");

            troca.Status = StatusTrocaPlantaoEnum.Recusada;

            await _repository.Editar(troca);

            await _repository.Cadastrar(new TrocaHistoricoModel
            {
                TrocaPlantao = troca,
                Evento = EventoHistoricoEnum.Recusada,
                UsuarioId = userId,
                Observacao = "Destinatário recusou"
            });

            return await _unit.Commit();
        }

        public async Task<bool> EnviarParaAprovacao(long id, long userId)
        {
            var troca = await _repository.ConsultarTrocaCompleta(id);

            if (troca is null)
                throw new Exception("Precisa ser uma troca existente");

            if (troca.Plantao.Setor.RepresentanteId != userId)
                throw new Exception("Apenas o representante pode enviar para aprovação");

            if (troca.Status != StatusTrocaPlantaoEnum.Aceita)
                throw new Exception("Troca precisa estar aceita");


            troca.Status = StatusTrocaPlantaoEnum.AguardandoAprovacao;

            await _repository.Editar(troca);

            await _repository.Cadastrar(new TrocaHistoricoModel
            {
                TrocaPlantao = troca,
                Evento = EventoHistoricoEnum.AguardandoAprovacao,
                UsuarioId = userId,
                Observacao = "Enviado para aprovação"
            });

            return await _unit.Commit();
        }

        public async Task<bool> Aprovar(long id, long userId)
        {
            var troca = await _repository.ConsultarTrocaCompleta(id);

            if (troca is null)
                throw new Exception("Precisa ser uma troca existente");

            if (troca.Plantao.Setor.RepresentanteId != userId)
                throw new Exception("Apenas o representante pode aprovar");

            if (troca.Status != StatusTrocaPlantaoEnum.AguardandoAprovacao)
                throw new Exception("Troca não está aguardando aprovação");


            troca.Status = StatusTrocaPlantaoEnum.Aprovada;
            troca.Plantao.ProfissionalResponsavelId = troca.SolicitanteId;

            await _repository.Editar(troca);
            await _repository.Editar(troca.Plantao);

            await _repository.Cadastrar(new TrocaHistoricoModel
            {
                TrocaPlantao = troca,
                Evento = EventoHistoricoEnum.Aprovada,
                UsuarioId = userId,
                Observacao = "Aprovada"
            });

            return await _unit.Commit();
        }

        public async Task<bool> Reprovar(long id, long userId)
        {
            var troca = await _repository.ConsultarTrocaCompleta(id);

            if (troca is null)
                throw new Exception("Precisa ser uma troca existente");

            if (troca.Plantao.Setor.RepresentanteId != userId)
                throw new Exception("Apenas o representante pode reprovar");

            if (troca.Status != StatusTrocaPlantaoEnum.AguardandoAprovacao)
                throw new Exception("Troca não está aguardando aprovação");

            troca.Status = StatusTrocaPlantaoEnum.Reprovada;

            await _repository.Editar(troca);

            await _repository.Cadastrar(new TrocaHistoricoModel
            {
                TrocaPlantao = troca,
                Evento = EventoHistoricoEnum.Reprovada,
                UsuarioId = userId,
                Observacao = "Reprovada"
            });

            return await _unit.Commit();
        }

        public async Task<TrocaPlantaoModel?> Deletar(long id)
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
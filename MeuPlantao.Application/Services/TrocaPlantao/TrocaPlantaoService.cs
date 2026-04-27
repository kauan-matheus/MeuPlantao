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

        public async Task<ServiceResponse<List<TrocaPlantaoModel>>> Consultar()
        {
            try
            {
                var data = await _repository.Consultar<TrocaPlantaoModel>()
                    .OrderBy(p => p.Id)
                    .ToListAsync();

                return ServiceResponse<List<TrocaPlantaoModel>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<TrocaPlantaoModel>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<TrocaPlantaoModel>> ConsultarId(long id)
        {
            try
            {
                var data = await _repository.ConsultarPorId<TrocaPlantaoModel>(id);

                if (data is null)
                    return ServiceResponse<TrocaPlantaoModel>.BadRequest("Registro não encontrado");

                return ServiceResponse<TrocaPlantaoModel>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<TrocaPlantaoModel>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Cadastrar(RequestTrocaPlantaoRegisterJson troca, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var plantao = await _repository.ConsultarPorId<PlantaoModel>(troca.PlantaoId);
                var prof = await _profRepository.ConsultarPorUserId(userId);

                if (plantao is null)
                    return ServiceResponse<bool>.BadRequest("plantao não existe");

                if (prof is null)
                    return ServiceResponse<bool>.BadRequest("É necessario estar logado como um profissional");

                if (plantao.ProfissionalResponsavelId is null)
                    return ServiceResponse<bool>.BadRequest("Plantao não possui responsavel para que possa ocorrer uma troca");

                if (prof.Id == plantao.ProfissionalResponsavelId)
                    return ServiceResponse<bool>.BadRequest("Destinatario é mesmo usuario que solicitante");

                var novo = new TrocaPlantaoModel
                {
                    PlantaoId = troca.PlantaoId,
                    SolicitanteId = prof.Id,
                    DestinatarioId = (long)plantao.ProfissionalResponsavelId,
                    Status = StatusTrocaPlantaoEnum.Pendente,
                    Motivo = troca.Motivo,
                };

                var novoHistorico = new TrocaHistoricoModel
                {
                    Evento = EventoHistoricoEnum.Criada,
                    UsuarioId = userId,
                    Observacao = "Troca criada"
                };

                await _repository.CadastrarComHistorico(novo, novoHistorico);

                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Aceitar(long id, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var troca = await _repository.ConsultarPorId<TrocaPlantaoModel>(id);
                var prof = await _profRepository.ConsultarPorUserId(userId);

                if (troca is null)
                    return ServiceResponse<bool>.BadRequest("Troca não existe");

                if (prof is null)
                    return ServiceResponse<bool>.BadRequest("É necessario estar logado como um profissional");

                if (prof.Id != troca.DestinatarioId)
                    return ServiceResponse<bool>.BadRequest("Apenas o destinatário pode aceitar");

                if (troca.Status != StatusTrocaPlantaoEnum.Pendente)
                    return ServiceResponse<bool>.BadRequest("Troca não pode ser aceita");

                troca.Status = StatusTrocaPlantaoEnum.Aceita;

                var novoHistorico = new TrocaHistoricoModel
                {
                    Evento = EventoHistoricoEnum.Aceita,
                    UsuarioId = userId,
                    Observacao = "Destinatário aceitou"
                };

                await _repository.EditarComHistorico(troca, novoHistorico);

                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Recusar(long id, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var troca = await _repository.ConsultarPorId<TrocaPlantaoModel>(id);
                var prof = await _profRepository.ConsultarPorUserId(userId);

                if (troca is null)
                    return ServiceResponse<bool>.BadRequest("Troca não existe");

                if (prof is null)
                    return ServiceResponse<bool>.BadRequest("É necessario estar logado como um profissional");

                if (prof.Id != troca.DestinatarioId)
                    return ServiceResponse<bool>.BadRequest("Apenas o destinatário pode recusar");

                if (troca.Status != StatusTrocaPlantaoEnum.Pendente)
                    return ServiceResponse<bool>.BadRequest("Troca não pode ser recusada");

                troca.Status = StatusTrocaPlantaoEnum.Recusada;

                var novoHistorico = new TrocaHistoricoModel
                {
                    Evento = EventoHistoricoEnum.Recusada,
                    UsuarioId = userId,
                    Observacao = "Destinatário recusou"
                };

                await _repository.EditarComHistorico(troca, novoHistorico);

                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> EnviarParaAprovacao(long id, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var troca = await _repository.ConsultarTrocaCompleta(id);

                if (troca is null)
                    return ServiceResponse<bool>.BadRequest("Precisa ser uma troca existente");

                if (troca.Plantao.Setor.RepresentanteId != userId)
                    return ServiceResponse<bool>.BadRequest("Apenas o representante pode enviar para aprovação");

                if (troca.Status != StatusTrocaPlantaoEnum.Aceita)
                    return ServiceResponse<bool>.BadRequest("Troca precisa estar aceita");

                troca.Status = StatusTrocaPlantaoEnum.AguardandoAprovacao;

                var novoHistorico = new TrocaHistoricoModel
                {
                    Evento = EventoHistoricoEnum.AguardandoAprovacao,
                    UsuarioId = userId,
                    Observacao = "Enviado para aprovação"
                };

                await _repository.EditarComHistorico(troca, novoHistorico);

                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Aprovar(long id, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var troca = await _repository.ConsultarTrocaCompleta(id);

                if (troca is null)
                    return ServiceResponse<bool>.BadRequest("Precisa ser uma troca existente");

                if (troca.Plantao.Setor.RepresentanteId != userId)
                    return ServiceResponse<bool>.BadRequest("Apenas o representante pode aprovar");

                if (troca.Status != StatusTrocaPlantaoEnum.AguardandoAprovacao)
                    return ServiceResponse<bool>.BadRequest("Troca não está aguardando aprovação");

                troca.Status = StatusTrocaPlantaoEnum.Aprovada;
                troca.Plantao.ProfissionalResponsavelId = troca.SolicitanteId;

                await _repository.Editar(troca.Plantao);

                var novoHistorico = new TrocaHistoricoModel
                {
                    Evento = EventoHistoricoEnum.Aprovada,
                    UsuarioId = userId,
                    Observacao = "Aprovada"
                };

                await _repository.EditarComHistorico(troca, novoHistorico);

                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Reprovar(long id, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var troca = await _repository.ConsultarTrocaCompleta(id);

                if (troca is null)
                    return ServiceResponse<bool>.BadRequest("Precisa ser uma troca existente");

                if (troca.Plantao.Setor.RepresentanteId != userId)
                    return ServiceResponse<bool>.BadRequest("Apenas o representante pode reprovar");

                if (troca.Status != StatusTrocaPlantaoEnum.AguardandoAprovacao)
                    return ServiceResponse<bool>.BadRequest("Troca não está aguardando aprovação");

                troca.Status = StatusTrocaPlantaoEnum.Reprovada;

                var novoHistorico = new TrocaHistoricoModel
                {
                    Evento = EventoHistoricoEnum.Reprovada,
                    UsuarioId = userId,
                    Observacao = "Reprovada"
                };

                await _repository.EditarComHistorico(troca, novoHistorico);

                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<TrocaPlantaoModel>> Deletar(long id)
        {
            try
            {
                var existente = await _repository.ConsultarPorId<TrocaPlantaoModel>(id);

                if (existente is null)
                    return ServiceResponse<TrocaPlantaoModel>.BadRequest("Registro não encontrado");

                await _repository.Excluir(existente);
                await _unit.Commit();

                return ServiceResponse<TrocaPlantaoModel>.Ok(existente);
            }
            catch (Exception ex)
            {
                return ServiceResponse<TrocaPlantaoModel>.Error(ex.Message);
            }
        }
    }
}
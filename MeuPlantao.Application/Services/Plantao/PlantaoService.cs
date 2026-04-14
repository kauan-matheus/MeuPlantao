using System.Runtime.CompilerServices;
using MeuPlantao.Application.Services.PlantaoHistorico;
using MeuPlantao.Application.Services.TrocaHistorico;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.Plantao
{
    public class PlantaoService : IPlantaoService
    {
        private readonly IPlantaoRepository _repository;
        private readonly IUnitOfWork _unit;
        private readonly IProfRepository _profRepository;

        public PlantaoService(IPlantaoRepository repository, IUnitOfWork unit, IProfRepository profRepository)
        {
            _repository = repository;
            _unit = unit;
            _profRepository = profRepository;
        }

        public async Task<ServiceResponse<List<ResponsePlantaoJson>>> Consultar()
        {
            try
            {
                var query = _repository.Consultar<PlantaoModel>()
                    .OrderBy(p => p.Id);

                var plantoes = await query.Select(p => new ResponsePlantaoJson
                {
                    Valor = p.Valor,
                    SetorId = p.SetorId,
                    Inicio = p.Fim,
                    Fim = p.Fim
                }).ToListAsync();

                return ServiceResponse<List<ResponsePlantaoJson>>.Ok(plantoes);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<ResponsePlantaoJson>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<ResponsePlantaoJson>> ConsultarId(long id)
        {
            try
            {
                var result = await _repository.ConsultarPorId<PlantaoModel>(id);

                if (result is null)
                    return ServiceResponse<ResponsePlantaoJson>.BadRequest("Plantao nao existe");

                if (result.ProfissionalResponsavelId is null)
                    return ServiceResponse<ResponsePlantaoJson>.Ok(new ResponsePlantaoJson
                    {
                        Valor = result.Valor,
                        SetorId = result.SetorId,
                        Inicio = result.Inicio,
                        Fim = result.Fim,
                    });

                return ServiceResponse<ResponsePlantaoJson>.Ok(new ResponsePlantaoJson
                {
                    Valor = result.Valor,
                    SetorId = result.SetorId,
                    Inicio = result.Inicio,
                    Fim = result.Fim,
                    ProfissionalResponsavelId = (long)result.ProfissionalResponsavelId
                });
            }
            catch (Exception ex)
            {
                return ServiceResponse<ResponsePlantaoJson>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Cadastrar(RequestPlantaoRegisterJson plantao, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var setorExistente = await _repository.ConsultarPorId<SetorModel>(plantao.SetorId);

                if (setorExistente is null)
                    return ServiceResponse<bool>.BadRequest("setor não existe");

                if (setorExistente.RepresentanteId != userId)
                    return ServiceResponse<bool>.BadRequest("Apenas o representate do setor é capaz de registrar plantoes para o respectivo setor");

                var novo = new PlantaoModel
                {
                    SetorId = plantao.SetorId,
                    Valor = plantao.Valor,
                    Inicio = plantao.Inicio,
                    Fim = plantao.Fim,
                    Status = StatusPlantaoEnum.AguardandoProfissional
                };

                var novoHistorico = new PlantaoHistoricoModel
                {
                    Evento = EventoPlantaoHistoricoEnum.Criada,
                    UsuarioId = userId,
                    Observacao = "Plantao criado"
                };

                await _repository.CadastrarComHistorico(novo, novoHistorico);

                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<bool>.Ok(true);
            }
            catch
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<bool>.Error("Nao foi possivel cadastrar esse plantao");
            }
        }

        public async Task<ServiceResponse<bool>> Editar(RequestPlantaoRegisterJson plantao, long userId)
        {
            try
            {
                var setorExistente = await _repository.ConsultarPorId<SetorModel>(plantao.SetorId);

                if (setorExistente is null)
                    return ServiceResponse<bool>.BadRequest("Setor nao existe");

                if (setorExistente.RepresentanteId != userId)
                    return ServiceResponse<bool>.BadRequest("Plantao apenas pode ser editado pelo representante do setor");

                var novo = new PlantaoModel
                {
                    Id = plantao.Id,
                    SetorId = plantao.SetorId,
                    Setor = setorExistente,
                    Inicio = plantao.Inicio,
                    Fim = plantao.Fim,
                };

                await _repository.Editar(novo);
                var saved = await _unit.Commit();

                if (saved)
                    return ServiceResponse<bool>.Ok(true);

                return ServiceResponse<bool>.Error("Nao foi possivel editar esse plantao");
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<ResponsePlantaoJson>> Deletar(long id)
        {
            try
            {
                var existente = await _repository.ConsultarPorId<PlantaoModel>(id);

                if (existente is null)
                    return ServiceResponse<ResponsePlantaoJson>.BadRequest("Plantao nao existe");

                await _repository.Excluir(existente);
                var saved = await _unit.Commit();

                if (saved)
                {
                    if (existente.ProfissionalResponsavelId is null)
                        return ServiceResponse<ResponsePlantaoJson>.Ok(new ResponsePlantaoJson
                        {
                            Valor = existente.Valor,
                            SetorId = existente.SetorId,
                            Inicio = existente.Inicio,
                            Fim = existente.Fim,
                        });

                    return ServiceResponse<ResponsePlantaoJson>.Ok(new ResponsePlantaoJson
                    {
                        Valor = existente.Valor,
                        SetorId = existente.SetorId,
                        Inicio = existente.Inicio,
                        Fim = existente.Fim,
                        ProfissionalResponsavelId = (long)existente.ProfissionalResponsavelId
                    });
                }

                return ServiceResponse<ResponsePlantaoJson>.Error("Nao foi possivel deletar esse plantao");
            }
            catch (Exception ex)
            {
                return ServiceResponse<ResponsePlantaoJson>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Solicitar(long id, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var plantao = await _repository.ConsultarPorId<PlantaoModel>(id);
                var prof = await _profRepository.ConsultarPorUserId(userId);

                if (plantao is null)
                    return ServiceResponse<bool>.BadRequest("plantao não existe");

                if (prof is null)
                    return ServiceResponse<bool>.BadRequest("É necessario estar logado como um profissional");

                if (plantao.Status != StatusPlantaoEnum.AguardandoProfissional)
                    return ServiceResponse<bool>.BadRequest("plantao nao esta em estado de aguardando usuario");

                plantao.Status = StatusPlantaoEnum.AguardandoRespostaSolicitacao;
                plantao.SolicitanteId = prof.Id;

                var novoHistorico = new PlantaoHistoricoModel
                {
                    Evento = EventoPlantaoHistoricoEnum.AguardandoRespostaSolicitacao,
                    UsuarioId = userId,
                    Observacao = "Solicitacao criado"
                };

                await _repository.EditarComHistorico(plantao, novoHistorico);

                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<bool>.Ok(true);
            }
            catch
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<bool>.Error("Nao foi possivel socilitar esse plantao");
            }
        }

        public async Task<ServiceResponse<bool>> AceitarSolicitacao(long id, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var plantao = await _repository.ConsultarPlantaoCompleto(id);

                if (plantao is null)
                    return ServiceResponse<bool>.BadRequest("Plantao nao existe");

                if (plantao.Setor.RepresentanteId != userId)
                    return ServiceResponse<bool>.BadRequest("Apenas o representante pode aceitar solicitacoes");

                if (plantao.Status != StatusPlantaoEnum.AguardandoRespostaSolicitacao)
                    return ServiceResponse<bool>.BadRequest("Plantao não esta aguardando repostas de solicitacao");

                plantao.Status = StatusPlantaoEnum.Ativo;
                plantao.ProfissionalResponsavelId = plantao.SolicitanteId;
                plantao.SolicitanteId = null;

                var novoHistorico = new PlantaoHistoricoModel
                {
                    Evento = EventoPlantaoHistoricoEnum.Aceito,
                    UsuarioId = userId,
                    Observacao = "Solicitacao aceita"
                };

                await _repository.EditarComHistorico(plantao, novoHistorico);

                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<bool>.Ok(true);
            }
            catch
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<bool>.Error("Nao foi possivel aceitar essa solicitacao");
            }
        }

        public async Task<ServiceResponse<bool>> RecusarSolicitacao(long id, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var plantao = await _repository.ConsultarPlantaoCompleto(id);

                if (plantao is null)
                    return ServiceResponse<bool>.BadRequest("Plantao nao existe");

                if (plantao.Setor.RepresentanteId != userId)
                    return ServiceResponse<bool>.BadRequest("Apenas o representante pode recusar solicitacoes");

                if (plantao.Status != StatusPlantaoEnum.AguardandoRespostaSolicitacao)
                    return ServiceResponse<bool>.BadRequest("Plantao não esta aguardando repostas de solicitacao");

                plantao.Status = StatusPlantaoEnum.AguardandoProfissional;
                plantao.SolicitanteId = null;

                var novoHistorico = new PlantaoHistoricoModel
                {
                    Evento = EventoPlantaoHistoricoEnum.Recusado,
                    UsuarioId = userId,
                    Observacao = "Solicita recusada"
                };

                await _repository.EditarComHistorico(plantao, novoHistorico);

                await _unit.Commit();
                await _unit.CommitTransaction();

                return ServiceResponse<bool>.Ok(true);
            }
            catch
            {
                await _unit.RollbackTransaction();
                return ServiceResponse<bool>.Error("Nao foi possivel recusar essa solicitacao");
            }
        }
    }
}
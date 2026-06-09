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
                    Id = p.Id,
                    Date = p.Inicio.ToString("dd/MM/yyyy"),
                    Start = p.Inicio.ToString("HH:mm"),
                    Duration = (p.Fim - p.Inicio).ToString(@"hh\:mm"),
                    Locale = p.Setor.Estabelecimento.Nome,
                    Sector = p.Setor.Nome,
                    Value = p.Valor,
                    Responsable = p.Setor.Representante.Email,
                    OnDuty = p.ProfissionalResponsavel != null 
                        ? p.ProfissionalResponsavel.Nome 
                        : "Disponivel",
                    Status = (int)p.Status
                }).ToListAsync();

                return ServiceResponse<List<ResponsePlantaoJson>>.Ok(plantoes);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<ResponsePlantaoJson>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<ResponseSolicitacoesJson>>> ConsultarSolicitacoes(long id)
        {
            try
            {
                var plantao = await _repository.ConsultarPorId<PlantaoModel>(id);
                if (plantao is null)
                    return ServiceResponse<List<ResponseSolicitacoesJson>>.BadRequest("Plantao nao existe");

                var solicitacoes = await _repository.ConsultarSolicitacoesPorPlantao(id).ToListAsync();
                var response = solicitacoes.Select(s => new ResponseSolicitacoesJson
                {
                    PlantaoId = s.PlantaoId,
                    ProfissionalId = s.ProfissionalId
                }).ToList();

                return ServiceResponse<List<ResponseSolicitacoesJson>>.Ok(response);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<ResponseSolicitacoesJson>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<ResponsePlantaoJson>> ConsultarId(long id)
        {
            try
            {
                var result = await _repository.ConsultarPorId<PlantaoModel>(id);

                if (result is null)
                    return ServiceResponse<ResponsePlantaoJson>.BadRequest("Plantao nao existe");

                
                return ServiceResponse<ResponsePlantaoJson>.Ok(new ResponsePlantaoJson
                {
                    Id = result.Id,
                    Date = result.Inicio.ToString("dd/MM/yyyy"),
                    Start = result.Inicio.ToString("HH:mm"),
                    Duration = (result.Fim - result.Inicio).ToString(@"hh\:mm"),
                    Locale = result.Setor.Estabelecimento.Nome,
                    Sector = result.Setor.Nome,
                    Value = result.Valor,
                    Responsable = result.Setor.Representante.Email,
                    OnDuty = result.ProfissionalResponsavel != null 
                        ? result.ProfissionalResponsavel.Nome 
                        : "Disponivel",
                    Status = (int)result.Status
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
                    Status = StatusPlantaoEnum.Disponivel
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

        public async Task<ServiceResponse<PlantaoModel>> Deletar(long id)
        {
            try
            {
                var existente = await _repository.ConsultarPorId<PlantaoModel>(id);

                if (existente is null)
                    return ServiceResponse<PlantaoModel>.BadRequest("Plantao nao existe");

                await _repository.Excluir(existente);
                var saved = await _unit.Commit();

                if (saved)
                {
                    if (existente.ProfissionalResponsavelId is null)
                        return ServiceResponse<PlantaoModel>.Ok(existente);
                }

                return ServiceResponse<PlantaoModel>.Error("Nao foi possivel deletar esse plantao");
            }
            catch (Exception ex)
            {
                return ServiceResponse<PlantaoModel>.Error(ex.Message);
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

                if (plantao.Status != StatusPlantaoEnum.Disponivel)
                    return ServiceResponse<bool>.BadRequest("Plantao nao esta mais disponivel"); 
                
                var novaSolicitacao = new SolicitacaoModel
                {
                    PlantaoId = plantao.Id,
                    ProfissionalId = prof.Id
                };

                var novoHistorico = new PlantaoHistoricoModel
                {
                    Evento = EventoPlantaoHistoricoEnum.AguardandoRespostaSolicitacao,
                    UsuarioId = userId,
                    Observacao = "Solicitacao criado"
                };


                await _repository.EditarComHistorico(plantao, novoHistorico);
                await _repository.Cadastrar(novaSolicitacao);

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

        public async Task<ServiceResponse<bool>> AceitarSolicitacao(long id, long solicitanteId, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var plantao = await _repository.ConsultarPlantaoCompleto(id);

                if (plantao is null)
                    return ServiceResponse<bool>.BadRequest("Plantao nao existe");

                var solicitacao = await _repository.ConsultarSolicitacaoPorPlantaoSolicitante(id, solicitanteId);

                var usuarioLogado = await _repository.ConsultarPorId<UserModel>(userId);
                bool isAdminOuGestor = usuarioLogado?.Role == RoleEnum.Admin || usuarioLogado?.Role == RoleEnum.Gestor;

                if (plantao.Setor.RepresentanteId != userId && !isAdminOuGestor)
                    return ServiceResponse<bool>.BadRequest("Apenas o representante pode aceitar solicitacoes");
                if (solicitacao is null)
                    return ServiceResponse<bool>.BadRequest("Esse profissional nao fez solicitacao para esse plantao");

                if (plantao.Status == StatusPlantaoEnum.Inativo)
                    return ServiceResponse<bool>.BadRequest("Plantao nao esta mais disponivel"); 

                plantao.Status = StatusPlantaoEnum.Ativo;
                plantao.ProfissionalResponsavelId = solicitanteId;

                var novoHistorico = new PlantaoHistoricoModel
                {
                    Evento = EventoPlantaoHistoricoEnum.Aceito,
                    UsuarioId = userId,
                    Observacao = "Solicitacao aceita"
                };

                await _repository.EditarComHistorico(plantao, novoHistorico);
                await _repository.Excluir(solicitacao);

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

        public async Task<ServiceResponse<bool>> RecusarSolicitacao(long id, long solicitanteId, long userId)
        {
            await _unit.BeginTransaction();

            try
            {
                var plantao = await _repository.ConsultarPlantaoCompleto(id);

                if (plantao is null)
                    return ServiceResponse<bool>.BadRequest("Plantao nao existe");

                var solicitacao = await _repository.ConsultarSolicitacaoPorPlantaoSolicitante(id, solicitanteId);

                var usuarioLogado = await _repository.ConsultarPorId<UserModel>(userId);
                bool isAdminOuGestor = usuarioLogado?.Role == RoleEnum.Admin || usuarioLogado?.Role == RoleEnum.Gestor;

                if (plantao.Setor.RepresentanteId != userId && !isAdminOuGestor)
                    return ServiceResponse<bool>.BadRequest("Apenas o representante pode recusar solicitacoes");
                if (solicitacao is null)
                    return ServiceResponse<bool>.BadRequest("Esse profissional nao fez solicitacao para esse plantao");

                var novoHistorico = new PlantaoHistoricoModel
                {
                    Evento = EventoPlantaoHistoricoEnum.Recusado,
                    UsuarioId = userId,
                    Observacao = "Solicita recusada"
                };

                await _repository.EditarComHistorico(plantao, novoHistorico);
                await _repository.Excluir(solicitacao);

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
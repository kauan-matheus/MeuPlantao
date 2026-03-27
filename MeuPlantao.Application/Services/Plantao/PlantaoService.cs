using System.Runtime.CompilerServices;
using MeuPlantao.Application.Services.PlantaoHistorico;
using MeuPlantao.Application.Services.TrocaHistorico;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.Plantao
{
    public class PlantaoService : IPlantaoService
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unit;
        private readonly IProfRepository _profRepository;

        public PlantaoService(IRepository repository, IUnitOfWork unit, IProfRepository profRepository)
        {
            _repository = repository;
            _unit = unit;
            _profRepository = profRepository;

        }

        public async Task<List<PlantaoModel>> Consultar()
        {
            return await _repository.Consultar<PlantaoModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<PlantaoModel?> ConsultarId(long id)
        {
            return await _repository.ConsultarPorId<PlantaoModel>(id);
        }

        public async Task<bool> Cadastrar(RequestPlantaoRegisterJson plantao, long userId)
        {
            // Valida se o setor e o profissional existem antes de criar o plantão
            var setorExistente = await _repository.ConsultarPorId<SetorModel>(plantao.SetorId);

            if (setorExistente is null)
                throw new Exception("setor não existe");

            var novo = new PlantaoModel
            {
                SetorId = plantao.SetorId,
                Setor = setorExistente,
                Inicio = plantao.Inicio,
                Fim = plantao.Fim,
                Status = StatusPlantaoEnum.AguardandoProfissional
            };

            await _repository.Cadastrar(novo);

            Console.WriteLine($"ID do plantão: {novo.Id}");
    
            await _repository.Cadastrar(new PlantaoHistoricoModel
            {
                Plantao = novo,
                Evento = EventoPlantaoHistoricoEnum.Criada,
                UsuarioId = userId,
                Observacao = "Plantao criado"
            });

            return await _unit.Commit();
        }

        public async Task<bool> Editar(RequestPlantaoRegisterJson plantao)
        {
            // Valida se o setor e o profissional existem antes de editar o plantão
            var setorExistente = await _repository.ConsultarPorId<SetorModel>(plantao.SetorId);

            if (setorExistente is null)
                return false;

            var novo = new PlantaoModel
            {
                Id = plantao.Id,
                SetorId = plantao.SetorId,
                Setor = setorExistente,
                Inicio = plantao.Inicio,
                Fim = plantao.Fim,
            };

            await _repository.Editar(novo);
            return await _unit.Commit();
        }

        public async Task<PlantaoModel?> Deletar(long id)
        {
            var existente = await ConsultarId(id);
            if (existente is null)
                return null;

            await _repository.Excluir(existente);
            await _unit.Commit();
            return existente;
        }

        public async Task<bool> Solicitar(long id, long userId)
        {
            var plantao = await _repository
                .ConsultarPorId<PlantaoModel>(id);

            var prof = await _profRepository.ConsultarPorUserId(userId);

            if (plantao is null)
                throw new Exception("plantao não existe");
            if (prof is null)
                throw new Exception("É necessario estar logado como um profissional");

            if (plantao.Status != StatusPlantaoEnum.AguardandoProfissional)
                throw new Exception("plantao nao esta em estado de aguardando usuario");

            plantao.Status = StatusPlantaoEnum.AguardandoRespostaSolicitacao;
            plantao.SolicitanteId = prof.Id;

            await _repository.Editar(plantao);

            await _repository.Cadastrar(new PlantaoHistoricoModel
            {
                Plantao = plantao,
                Evento = EventoPlantaoHistoricoEnum.AguardandoRespostaSolicitacao,
                UsuarioId = userId,
                Observacao = "Solicitacao criado"
            });

            return await _unit.Commit();
        }

        public async Task<bool> AceitarSolicitacao(long id, long userId)
        {
            var plantao = await _repository
                .ConsultarPorId<PlantaoModel>(id);

            if (plantao is null)
                return false;
            
            var setor = await _repository
                .ConsultarPorId<SetorModel>(plantao.SetorId);

            // checa se setor existe, se sim checa se o usuario que estiver logado no momento é o responsavel pelo setor
            if (setor is null)
                return false;

            if (setor.RepresentanteId != userId)
                throw new Exception("Apenas o representante pode aceitar solicitacoes");

            if (plantao.Status != StatusPlantaoEnum.AguardandoRespostaSolicitacao)
                throw new Exception("Plantao não esta aguardando repostas de solicitacao");

            plantao.Status = StatusPlantaoEnum.Ativo;
            plantao.ProfissionalResponsavelId = plantao.SolicitanteId;
            plantao.SolicitanteId = null;

            await _repository.Editar<PlantaoModel>(plantao);

            await _repository.Cadastrar(new PlantaoHistoricoModel
            {
                Plantao = plantao,
                Evento = EventoPlantaoHistoricoEnum.Aceito,
                UsuarioId = userId,
                Observacao = "Solicitacao aceita"
            });

            return await _unit.Commit();
        }
        public async Task<bool> RecusarSolicitacao(long id, long userId)
        {
            var plantao = await _repository
                .ConsultarPorId<PlantaoModel>(id);

            if (plantao is null)
                return false;
            
            var setor = await _repository
                .ConsultarPorId<SetorModel>(plantao.SetorId);

            // checa se setor existe, se sim checa se o usuario que estiver logado no momento é o responsavel pelo setor
            if (setor == null)
                return false;

            if (setor.RepresentanteId != userId)
                throw new Exception("Apenas o representante pode recusar solicitacoes");

            if (plantao.Status != StatusPlantaoEnum.AguardandoRespostaSolicitacao)
                throw new Exception("Plantao não esta aguardando repostas de solicitacao");

            plantao.Status = StatusPlantaoEnum.AguardandoProfissional;
            plantao.SolicitanteId = null;

            await _repository.Editar(plantao);

            await _repository.Cadastrar(new PlantaoHistoricoModel
            {
                Plantao = plantao,
                Evento = EventoPlantaoHistoricoEnum.Recusado,
                UsuarioId = userId,
                Observacao = "Solicita recusada"
            });

            return await _unit.Commit();
        }
    }
}
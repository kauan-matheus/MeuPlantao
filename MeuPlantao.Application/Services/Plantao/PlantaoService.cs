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
        private readonly IPlantaoHistoricoService _historicoService;

        public PlantaoService(IRepository repository, IPlantaoHistoricoService historicoService)
        {
            _repository = repository;
            _historicoService = historicoService;

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
                return false;

            var novo = new PlantaoModel
            {
                SetorId = plantao.SetorId,
                Setor = setorExistente,
                Inicio = plantao.Inicio,
                Fim = plantao.Fim,
                Status = StatusPlantaoEnum.AguardandoProfissional
            };

            var response1 = await _repository.Cadastrar(novo);
            
            var response2 = await _historicoService.Cadastrar(new RequestPlantaoHistoricoRegisterJson
            {
                PlantaoId = novo.Id,
                Evento = EventoPlantaoHistoricoEnum.Criada,
                UsuarioId = userId,
                Observacao = "Plantao criado"
            });

            return response1 && response2;
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

            return await _repository.Editar(novo);
        }

        public async Task<PlantaoModel?> Deletar(long id)
        {
            var existente = await ConsultarId(id);
            if (existente is null)
                return null;

            await _repository.Excluir(existente);
            return existente;
        }

        public async Task<bool> Solicitar(long id, long userId)
        {
            var plantao = await _repository
                .ConsultarPorId<PlantaoModel>(id);
            var user = await _repository
                .ConsultarPorId<UserModel>(userId);

            if (plantao == null || user == null)
                throw new Exception("plantao ou usuarion não existe");

            if (plantao.Status != StatusPlantaoEnum.AguardandoProfissional)
                throw new Exception("plantao nao esta em estado de aguardando usuario");

            plantao.Status = StatusPlantaoEnum.AguardandoRespostaSolicitacao;
            plantao.SolicitanteId = userId;

            var response1 = await _repository.Editar(plantao);

            var response2 = await _historicoService.Cadastrar(new RequestPlantaoHistoricoRegisterJson
            {
                PlantaoId = plantao.Id,
                Evento = EventoPlantaoHistoricoEnum.AguardandoRespostaSolicitacao,
                UsuarioId = userId,
                Observacao = "Usuario agora deve aguardar resposta"
            });

            return response1 && response2;
        }

        public async Task<bool> AceitarSolicitacao(long id, long userId)
        {
            var plantao = await _repository
                .ConsultarPorId<PlantaoModel>(id);

            var user = await _repository
                .ConsultarPorId<UserModel>(userId);

            if (plantao == null || user == null)
                return false;
            
            var setor = await _repository
                .ConsultarPorId<SetorModel>(plantao.SetorId);

            // checa se setor existe, se sim checa se o usuario que estiver logado no momento é o responsavel pelo setor
            if (setor == null)
                return false;
            if (setor.RepresentanteId != userId)
                return false;


            if (plantao.Status != StatusPlantaoEnum.AguardandoRespostaSolicitacao)
                return false;

            plantao.Status = StatusPlantaoEnum.Ativo;
            plantao.ProfissionalResponsavelId = plantao.SolicitanteId;
            plantao.SolicitanteId = null;

            var response1 = await _repository.Editar(plantao);

            var response2 = await _historicoService.Cadastrar(new RequestPlantaoHistoricoRegisterJson
            {
                PlantaoId = plantao.Id,
                Evento = EventoPlantaoHistoricoEnum.Aceito,
                UsuarioId = userId,
                Observacao = "Usuario foi aceito"
            });

            return response1 && response2;
        }
        public async Task<bool> RecusarSolicitacao(long id, long userId)
        {
            var plantao = await _repository
                .ConsultarPorId<PlantaoModel>(id);

            var user = await _repository
                .ConsultarPorId<UserModel>(userId);

            if (plantao == null || user == null)
                return false;
            
            var setor = await _repository
                .ConsultarPorId<SetorModel>(plantao.SetorId);

            // checa se setor existe, se sim checa se o usuario que estiver logado no momento é o responsavel pelo setor
            if (setor == null)
                return false;
            if (setor.RepresentanteId != userId)
                return false;

            if (plantao.Status != StatusPlantaoEnum.AguardandoRespostaSolicitacao)
                return false;

            plantao.Status = StatusPlantaoEnum.AguardandoProfissional;
            plantao.SolicitanteId = null;

            var response1 = await _repository.Editar(plantao);

            var response2 = await _historicoService.Cadastrar(new RequestPlantaoHistoricoRegisterJson
            {
                PlantaoId = plantao.Id,
                Evento = EventoPlantaoHistoricoEnum.Recusado,
                UsuarioId = userId,
                Observacao = "Usuario foi recusado"
            });

            return response1 && response2;
        }
    }
}
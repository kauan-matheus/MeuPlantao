using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.TrocaPlantao
{
    public interface ITrocaPlantaoService
    {
        Task<ServiceResponse<List<TrocaPlantaoModel>>> Consultar();

        // Nullable pois o setor pode não ser encontrado
        Task<ServiceResponse<TrocaPlantaoModel>> ConsultarId(long id);

        Task<ServiceResponse<bool>> Cadastrar(RequestTrocaPlantaoRegisterJson troca, long id);
        Task<ServiceResponse<bool>> Aceitar(long id, long userId);
        Task<ServiceResponse<bool>> Recusar(long id, long userId);
        Task<ServiceResponse<bool>> EnviarParaAprovacao(long id, long userId);
        Task<ServiceResponse<bool>> Aprovar(long id, long userId);
        Task<ServiceResponse<bool>> Reprovar(long id, long userId);

        // Nullable pois retorna null se o setor não existir
        Task<ServiceResponse<TrocaPlantaoModel>> Deletar(long id);
    }
}
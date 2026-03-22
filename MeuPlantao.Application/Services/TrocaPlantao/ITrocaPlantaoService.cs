using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.TrocaPlantao
{
    public interface ITrocaPlantaoService
    {
        Task<List<TrocaPlantaoModel>> Consultar();

        // Nullable pois o setor pode não ser encontrado
        Task<TrocaPlantaoModel?> ConsultarId(long id);

        Task<bool> Cadastrar(RequestTrocaPlantaoRegisterJson troca, long id);
        Task<bool> Aceitar(long id, long userId);
        Task<bool> Recusar(long id, long userId);
        Task<bool> EnviarParaAprovacao(long id, long userId);
        Task<bool> Aprovar(long id, long userId);
        Task<bool> Reprovar(long id, long userId);

        // Nullable pois retorna null se o setor não existir
        Task<TrocaPlantaoModel?> Deletar(long id);
    }
}
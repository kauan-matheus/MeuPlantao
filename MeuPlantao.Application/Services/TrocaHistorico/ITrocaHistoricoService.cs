using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.TrocaHistorico
{
    public interface ITrocaHistoricoService
    {
        Task<ServiceResponse<List<TrocaHistoricoModel>>> Consultar();

        // Nullable pois o setor pode não ser encontrado
        Task<ServiceResponse<TrocaHistoricoModel>> ConsultarId(long id);

        Task<ServiceResponse<bool>> Cadastrar(RequestTrocaHistoricoRegisterJson troca);
        Task<ServiceResponse<bool>> Editar(RequestTrocaHistoricoRegisterJson troca);

        // Nullable pois retorna null se o setor não existir
        Task<ServiceResponse<TrocaHistoricoModel>> Deletar(long id);
    }
}
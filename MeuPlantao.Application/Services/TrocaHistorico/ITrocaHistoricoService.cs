using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.TrocaHistorico
{
    public interface ITrocaHistoricoService
    {
        Task<List<TrocaHistoricoModel>> Consultar();

        // Nullable pois o setor pode não ser encontrado
        Task<TrocaHistoricoModel?> ConsultarId(long id);

        Task<bool> Cadastrar(RequestTrocaHistoricoRegisterJson troca);
        Task<bool> Editar(RequestTrocaHistoricoRegisterJson troca);

        // Nullable pois retorna null se o setor não existir
        Task<TrocaHistoricoModel?> Deletar(long id);
    }
}
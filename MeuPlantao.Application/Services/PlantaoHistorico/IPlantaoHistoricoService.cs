using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.PlantaoHistorico
{
    public interface IPlantaoHistoricoService
    {
        Task<ServiceResponse<List<PlantaoHistoricoModel>>> Consultar();
        Task<ServiceResponse<PlantaoHistoricoModel>> ConsultarId(long id);
        Task<ServiceResponse<bool>> Cadastrar(RequestPlantaoHistoricoRegisterJson plantao);
        Task<ServiceResponse<bool>> Editar(RequestPlantaoHistoricoRegisterJson plantao);
        Task<ServiceResponse<PlantaoHistoricoModel>> Deletar(long id);
    }
}
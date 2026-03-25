using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.PlantaoHistorico
{
    public interface IPlantaoHistoricoService
    {
        Task<List<PlantaoHistoricoModel>> Consultar();
        Task<PlantaoHistoricoModel?> ConsultarId(long id);
        Task<bool> Cadastrar(RequestPlantaoHistoricoRegisterJson troca);
        Task<bool> Editar(RequestPlantaoHistoricoRegisterJson troca);
        Task<PlantaoHistoricoModel?> Deletar(long id);
    }
}
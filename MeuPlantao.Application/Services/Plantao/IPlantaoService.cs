using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.Plantao
{
    public interface IPlantaoService
    {
        Task<List<PlantaoModel>> Consultar();
        Task<PlantaoModel> ConsultarId(long id); 
        Task<bool> Cadastrar(RequestPlantaoRegisterJson model);
        Task<bool> Editar(RequestPlantaoRegisterJson model);
        Task<PlantaoModel?> Deletar(long id);
    }
}
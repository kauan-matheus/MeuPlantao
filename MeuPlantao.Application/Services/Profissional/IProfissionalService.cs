using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Communication.Dto.Requests;

namespace MeuPlantao.Application.Services.Profissional
{
    public interface IProfissionalService
    {
        Task<List<ProfissionalModel>> Consultar();
        Task<ProfissionalModel> ConsultarId(long id); 
        Task<bool> Cadastrar(RequestProfissionalRegisterJson model);
        Task<bool> Editar(RequestProfissionalRegisterJson model);
        Task<ProfissionalModel?> Deletar(long id);
    }
}
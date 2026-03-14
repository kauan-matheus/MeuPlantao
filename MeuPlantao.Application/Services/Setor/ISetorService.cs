using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.Setor
{
    public interface ISetorService
    {
        Task<List<SetorModel>> Consultar();
        Task<SetorModel> ConsultarId(long id); 
        Task<bool> Cadastrar(SetorModel model);
        Task<bool> Editar(SetorModel model);
        Task<SetorModel?> Deletar(long id);
    }
}
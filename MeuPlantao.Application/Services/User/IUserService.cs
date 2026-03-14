using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services.User
{
    public interface IUserService
    {
        Task<List<UserModel>> Consultar();
        Task<UserModel> ConsultarId(long id); 
        Task<bool> Cadastrar(UserModel model);
        Task<bool> Editar(UserModel model);
        Task<UserModel?> Deletar(long id);
    }
}
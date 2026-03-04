using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Domain.Interfaces
{
    public interface IRepository
    {
        IQueryable<T> Consultar<T>()where T : class;
        T ConsultarPorId<T>(long id)where T : class; 
        bool Cadastrar<T>(T model)where T : class;
        bool Editar<T>(T model)where T : class;
        bool Excluir<T>(T model)where T : class;
    }
}
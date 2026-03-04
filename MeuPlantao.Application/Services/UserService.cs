using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Interfaces;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Application.Services
{
    public class UserService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public List<UserModel> Consultar()
        {
            var responce = _repository.Consultar<UserModel>()
                .OrderBy(p => p.Id)
                .ToList();

            return responce;
        }

        public UserModel ConsultarId(int Id)
        {
            var responce = _repository.ConsultarPorId<UserModel>(Id);
            return responce;
        }

        public bool Cadastrar(UserModel user)
        {
            var responce = _repository.Cadastrar<UserModel>(user);
            return responce;
        }

        public bool Editar(UserModel user)
        {
            var responce = _repository.Editar<UserModel>(user);
            return responce;
        }

        public UserModel? Deletar(int Id)
        {
            var existente = ConsultarId(Id);
            if (existente != null)
            {
                _repository.Excluir<UserModel>(existente);
                return existente;
            }
            else
            {
                return null;
            }
        }
    }
}
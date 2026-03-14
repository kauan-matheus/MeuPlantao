using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.Setor
{
    public class SetorService : ISetorService
    {
        private readonly IRepository _repository;

        public SetorService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SetorModel>> Consultar()
        {
            var responce = await _repository.Consultar<SetorModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();

            return responce;
        }

        public async Task<SetorModel> ConsultarId(long Id)
        {
            var responce = await _repository.ConsultarPorId<SetorModel>(Id);
            return responce;
        }

        public async Task<bool> Cadastrar(SetorModel setor)
        {
            var responce = await _repository.Cadastrar<SetorModel>(setor);
            return responce;
        }

        public async Task<bool> Editar(SetorModel setor)
        {
            var responce = await _repository.Editar<SetorModel>(setor);
            return responce;
        }

        public async Task<SetorModel?> Deletar(long Id)
        {
            var existente = await ConsultarId(Id);
            if (existente != null)
            {
                await _repository.Excluir<SetorModel>(existente);
                return existente;
            }
            else
            {
                return null;
            }
        }
    }
}
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MeuPlantao.Application.Services.Profissional
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IRepository _repository;

        public ProfissionalService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProfissionalModel>> Consultar()
        {
            var responce = await _repository.Consultar<ProfissionalModel>()
                .OrderBy(p => p.Id)
                .ToListAsync();

            return responce;
        }

        public async Task<ProfissionalModel> ConsultarId(long Id)
        {
            var responce = await _repository.ConsultarPorId<ProfissionalModel>(Id);
            return responce;
        }

        public async Task<bool> Cadastrar(RequestProfissionalRegisterJson profissional)
        {
            var existente = await _repository.ConsultarPorId<UserModel>(profissional.UserId);
            if (existente != null){
                var novo = new ProfissionalModel
                {
                    Nome = profissional.Nome,
                    Crm = profissional.Crm,
                    Telefone = profissional.Telefone,
                    UserId = profissional.UserId,
                    User = existente,
                };

                var responce = await _repository.Cadastrar<ProfissionalModel>(novo);
                return responce;
            }

            return false;
        }

        public async Task<bool> Editar(RequestProfissionalRegisterJson profissional)
        {
            var existente = await _repository.ConsultarPorId<UserModel>(profissional.UserId);
            if (existente != null){
                var novo = new ProfissionalModel
                {
                    Id = profissional.Id,
                    Nome = profissional.Nome,
                    Crm = profissional.Crm,
                    Telefone = profissional.Telefone,
                    UserId = profissional.UserId,
                    User = existente,
                };

                var responce = await _repository.Editar<ProfissionalModel>(novo);
                return responce;
            }

            return false;
        }

        public async Task<ProfissionalModel?> Deletar(long Id)
        {
            var existente = await ConsultarId(Id);
            if (existente != null)
            {
                await _repository.Excluir<ProfissionalModel>(existente);
                return existente;
            }
            else
            {
                return null;
            }
        }
    }
}
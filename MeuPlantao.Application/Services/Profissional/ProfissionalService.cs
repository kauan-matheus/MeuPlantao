using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var novo = new ProfissionalModel
            {
                Nome = profissional.Nome,
                Crm = profissional.Crm,
                Telefone = profissional.Telefone,
                UserId = profissional.UserId,
            };

            var responce = await _repository.Cadastrar<ProfissionalModel>(novo);
            return responce;
        }

        public async Task<bool> Editar(RequestProfissionalRegisterJson profissional)
        {
            var novo = new ProfissionalModel
            {
                Nome = profissional.Nome,
                Crm = profissional.Crm,
                Telefone = profissional.Telefone,
                UserId = profissional.UserId,
            };

            var responce = await _repository.Editar<ProfissionalModel>(novo);
            return responce;
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
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;

namespace MeuPlantao.Application.Services
{
    public class ProfissionalService
    {
        private readonly IRepository _repository;

        public ProfissionalService(IRepository repository)
        {
            _repository = repository;
        }

        public List<ProfissionalModel> Consultar()
        {
            var responce = _repository.Consultar<ProfissionalModel>()
                .OrderBy(p => p.Id)
                .ToList();

            return responce;
        }

        public ProfissionalModel ConsultarId(int Id)
        {
            var responce = _repository.ConsultarPorId<ProfissionalModel>(Id);
            return responce;
        }

        public bool Cadastrar(RequestProfissionalRegisterJson profissional)
        {
            var novo = new ProfissionalModel
            {
                Nome = profissional.Nome,
                Crm = profissional.Crm,
                Telefone = profissional.Telefone,
                UserId = profissional.UserId,
            };

            var responce = _repository.Cadastrar<ProfissionalModel>(novo);
            return responce;
        }

        public bool Editar(RequestProfissionalRegisterJson profissional)
        {
            var novo = new ProfissionalModel
            {
                Nome = profissional.Nome,
                Crm = profissional.Crm,
                Telefone = profissional.Telefone,
                UserId = profissional.UserId,
            };

            var responce = _repository.Editar<ProfissionalModel>(novo);
            return responce;
        }

        public ProfissionalModel? Deletar(int Id)
        {
            var existente = ConsultarId(Id);
            if (existente != null)
            {
                _repository.Excluir<ProfissionalModel>(existente);
                return existente;
            }
            else
            {
                return null;
            }
        }
    }
}
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;

namespace MeuPlantao.Application.Services
{
    public class ProfissionalService
    {
        private readonly IProfissionalRepository _repository;

        public ProfissionalService(IProfissionalRepository profissionalRepository)
        {
            _repository = profissionalRepository;
        }

        public List<ProfissionalModel> Consultar()
        {
            var responce = _repository.GetProfissionais()
                .OrderBy(p => p.Id)
                .ToList();

            return responce;
        }

        public ProfissionalModel ConsultarId(int Id)
        {
            var responce = _repository.GetProfissionalById(Id);
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

            var responce = _repository.PostProfissional(novo);
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

            var responce = _repository.PutProfissional(novo);
            return responce;
        }

        public ProfissionalModel? Deletar(int Id)
        {
            var existente = ConsultarId(Id);
            if (existente != null)
            {
                _repository.DeleteProfissional(existente);
                return existente;
            }
            else
            {
                return null;
            }
        }
    }
}
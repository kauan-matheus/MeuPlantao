using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MeuPlantao.Communication.Dto.Requests;

namespace MeuPlantao.Application.Validators
{
    public class PlantaoRegisterValidator : AbstractValidator<RequestPlantaoRegisterJson>
    {
        public PlantaoRegisterValidator()
        {
            RuleFor(x => x.SetorId)
                .NotEmpty().WithMessage("Setor é obrigatorio");

            RuleFor(x => x.Inicio)
                .NotEmpty().WithMessage("Data de inicio é obrigatorio");

            RuleFor(x => x.Fim)
                .NotEmpty().WithMessage("Data de fim é obrigatorio");
        }
    }
}
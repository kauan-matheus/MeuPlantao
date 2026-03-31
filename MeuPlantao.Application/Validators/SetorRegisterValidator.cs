using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MeuPlantao.Communication.Dto.Requests;

namespace MeuPlantao.Application.Validators
{
    public class SetorRegisterValidator : AbstractValidator<RequestSetorRegisterJson>
    {
        public SetorRegisterValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatorio")
                .MaximumLength(100);

            RuleFor(x => x.RepresentanteId)
                .NotEmpty().WithMessage("Representante é obrigatorio");
        }
    }
}
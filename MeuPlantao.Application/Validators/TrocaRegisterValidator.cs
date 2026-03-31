using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MeuPlantao.Communication.Dto.Requests;

namespace MeuPlantao.Application.Validators
{
    public class TrocaRegisterValidator : AbstractValidator<RequestTrocaPlantaoRegisterJson>
    {
        public TrocaRegisterValidator()
        {
            RuleFor(x => x.PlantaoId)
                .NotEmpty().WithMessage("Plantao é obrigatorio");
        }
    }
}
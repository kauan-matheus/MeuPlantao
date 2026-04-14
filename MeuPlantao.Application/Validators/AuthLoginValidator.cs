using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MeuPlantao.Communication.Dto.Requests;

namespace MeuPlantao.Application.Validators
{
    public class AuthLoginValidator : AbstractValidator<RequestAuthLoginJson>
    {
        public AuthLoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatorio")
                .EmailAddress().WithMessage("Email invalido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatoria");
        }
    }
}
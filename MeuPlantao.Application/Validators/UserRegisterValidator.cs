using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MeuPlantao.Communication.Dto.Requests;

namespace MeuPlantao.Application.Validators
{
    public class UserRegisterValidator : AbstractValidator<RequestUserRegisterJson>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatorio")
                .EmailAddress().WithMessage("Email invalido")
                .MaximumLength(100);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatoria")
                .MinimumLength(6).WithMessage("A senha deve ter no minimo 6 caracteres");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role é obrigatorio");
        }


    }
}
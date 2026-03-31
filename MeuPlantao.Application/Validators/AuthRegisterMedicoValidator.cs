using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MeuPlantao.Communication.Dto.Requests;

namespace MeuPlantao.Application.Validators
{
    public class AuthRegisterMedicoValidator : AbstractValidator<RequestAuthRegisterMedicoJson>
    {
        public AuthRegisterMedicoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatorio")
                .EmailAddress().WithMessage("Email invalido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatoria")
                .MinimumLength(6).WithMessage("Senha deve ter no minimo 6 caracteres");
            
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatoria")
                .MaximumLength(100);

            RuleFor(x => x.Crm)
                .NotEmpty().WithMessage("Crm é obrigatoria")
                .MaximumLength(10);

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatoria")
                .MaximumLength(9);
        }
    }
}
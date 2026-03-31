using FluentValidation;
using MeuPlantao.Communication.Dto.Requests;

namespace MeuPlantao.Application.Validators
{
    public class AuthRegisterAdminValidator : AbstractValidator<RequestAuthRegisterAdminJson>
    {
        public AuthRegisterAdminValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatorio")
                .EmailAddress().WithMessage("Email invalido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatoria")
                .MinimumLength(6).WithMessage("senha deve ter no minimo 6 caracteres");
        }
    }
}